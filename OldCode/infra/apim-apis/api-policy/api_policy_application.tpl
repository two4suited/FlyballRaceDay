 <policies>
      <inbound>
          <base />         
          <set-backend-service id="apim-generated-policy" backend-id="${ApplicationName}" />
      </inbound>
      <backend>
          <base />
      </backend>
      <outbound>
          <base />
      </outbound>
      <on-error>
          <base />
      </on-error>
  </policies>