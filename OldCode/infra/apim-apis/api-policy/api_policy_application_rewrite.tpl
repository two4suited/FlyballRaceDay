 <policies>
      <inbound>
          <base />          
          <rewrite-uri id="1" template="/${RewritePath}" />
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