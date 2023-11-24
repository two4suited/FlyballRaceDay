module "appServicePlan_name" {
  source           = "../namer"
  application_name = var.application_name
  resource_type    = "plan"
  environment      = var.environment
  location         = var.location
}

resource "azurerm_service_plan" "function_serviceplan" {
  name                = module.appServicePlan_name.resource_name
  location            = var.location
  resource_group_name = var.ResourceGroupName
  os_type = "Linux"
  sku_name = "Y1"  
  tags = var.module_tags
}

variable "APIs" {
  type    = set(string)
  default = ["TournamentAPI","RaceAPI","RingAPI"]
}

module "FunctionAPI" {
  for_each = var.APIs
  FunctionAppName = "${var.application_name}-${each.value}"
  DotNetVersion = "7.0"
  source = "./functionapp"
  depends_on = [ azurerm_service_plan.function_serviceplan ]
  application_name = var.application_name
  location = var.location
  environment = var.environment
  ResourceGroupName = var.ResourceGroupName
  StorageAccountName = var.StorageAccountName
  StorageAccountPrimaryKey = var.StorageAccountPrimaryKey  
  AppServicePlanId = azurerm_service_plan.function_serviceplan.id
  module_tags = var.module_tags
  CosmosDatabaseConnectionString = var.CosmosDatabaseConnectionString
  CosmosDatabaseContainer = var.CosmosDatabaseContainer
  ApplicationInsightsConnectionString = var.ApplicationInsightsConnectionString
  ApplicationInsightsKey = var.ApplicationInsightsKey
  ApiManagementName = var.ApiManagementName  
}
