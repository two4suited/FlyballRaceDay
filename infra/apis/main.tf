module "saif-apimanagement-apis" {
  source            = "../apim-apis"
  APIName           = "api"
  OpenAPISpecFile   = file("${path.root}/apis/FlyballRaceDay.yml")
  Path              = "api"
  DisplayName       = "API's for FlyballRaceDay"
  RevisionID        = 1
  API_Polices       = local.api_policies
  ApiManagementName = var.ApiManagementName
  ResourceGroupName = var.ResourceGroupName
}
