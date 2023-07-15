module "saif-apimanagement-apis" {
  source            = "../apim-apis"
  APIName           = "api"
  OpenAPISpecFile   = file("${path.root}/apis/flyballraceday.yml")
  Path              = "api"
  DisplayName       = "API's for FlyballRaceDay"
  RevisionID        = 1
  API_Polices       = local.api_policies
  ApiManagementName = var.ApiManagementName
  ResourceGroupName = var.ResourceGroupName
}
