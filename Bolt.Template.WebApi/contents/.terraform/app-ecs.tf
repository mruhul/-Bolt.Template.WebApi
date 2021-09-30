
resource "aws_alb_target_group" "app" {
  name        = "tg-${var.group}-${var.app_name}-${var.env}"
  port        = var.app_port
  protocol    = "HTTP"
  vpc_id      = data.aws_vpc.main.id
  target_type = "ip"

  health_check {
    healthy_threshold   = "3"
    interval            = "30"
    protocol            = "HTTP"
    matcher             = "200"
    timeout             = "3"
    path                = var.app_health_check_path
    unhealthy_threshold = "2"
  }


  tags = {
    Group   = var.group
    Env     = var.env
    AppName = var.app_name
    Name    = "tg-${var.group}-${var.app_name}-${var.env}"
  }
}

resource "aws_alb_listener_rule" "app" {
  listener_arn = data.aws_alb_listener.main.arn

  action {
    type             = "forward"
    target_group_arn = aws_alb_target_group.app.arn
  }

  condition {
    path_pattern {
      values = ["/${var.app_name}/*"]
    }
  }
}

data "template_file" "app" {
  template = file("./.templates/ecs/app.json.tpl")

  vars = {
    app_name       = "${var.group}-${var.app_name}"
    app_image      = var.app_image
    app_port       = var.app_port
    fargate_cpu    = var.fargate_cpu
    fargate_memory = var.fargate_memory
    aws_region     = var.aws_region
    app_log_group  = aws_cloudwatch_log_group.app.name,
    app_env_name   = lookup({ "dev" = "development", "prd" = "production", "stg" = "staging" }, var.env, var.env)
  }
}

resource "aws_ecs_task_definition" "app" {
  family                   = "${var.group}-${var.app_name}-${var.env}-task"
  execution_role_arn       = data.aws_iam_role.ecs_service_role.arn
  network_mode             = "awsvpc"
  requires_compatibilities = ["FARGATE"]
  cpu                      = var.fargate_cpu
  memory                   = var.fargate_memory
  container_definitions    = data.template_file.app.rendered
  task_role_arn            = aws_iam_role.ecs_task_role.arn
}

resource "aws_ecs_service" "main" {
  name                               = "${var.group}-${var.app_name}-${var.env}-service"
  cluster                            = data.aws_ecs_cluster.main.id
  task_definition                    = aws_ecs_task_definition.app.arn
  desired_count                      = var.app_count
  launch_type                        = "FARGATE"
  deployment_minimum_healthy_percent = 50
  deployment_maximum_percent         = 200
  scheduling_strategy                = "REPLICA"
  force_new_deployment               = true

  network_configuration {
    security_groups  = [data.aws_security_group.ecs_tasks.id]
    subnets          = data.aws_subnet_ids.private.ids
    assign_public_ip = true
  }

  load_balancer {
    target_group_arn = aws_alb_target_group.app.id
    container_name   = "${var.group}-${var.app_name}"
    container_port   = var.app_port
  }

  lifecycle {
    ignore_changes = [
      desired_count
    ]
  }
}

