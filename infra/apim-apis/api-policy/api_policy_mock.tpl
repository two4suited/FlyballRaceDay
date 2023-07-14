
    <policies>
      <inbound>
          <base />
          <mock-response status-code="${status_code}" content-type="application/json" />
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
