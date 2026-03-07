POST /api/leads/search
Content-Type: application/json

{
  "pageNumber": 1,
  "pageSize": 10,
  "sortBy": "DateOpened",
  "sortDirection": "Desc",
  "filters": [
    {
      "id": "FirstName",
      "label": "First Name",
      "type": "string",
      "controlType": "text",
      "value": "John",
      "options": [],
      "operation": "Contains",
      "logicalOperation": "OR"
    },
    {
      "id": "LastName",
      "label": "Last Name",
      "type": "string",
      "controlType": "text",
      "value": "Smith",
      "options": [],
      "operation": "Contains",
      "logicalOperation": "OR"
    },
    {
      "id": "Status",
      "label": "Lead Status",
      "type": "string",
      "controlType": "dropdown",
      "value": "Qualified",
      "options": [],
      "operation": "Eq",
      "logicalOperation": "AND"
    },
    {
      "id": "LeadSource",
      "label": "Lead Source",
      "type": "string",
      "controlType": "dropdown",
      "value": "Website",
      "options": [],
      "operation": "Eq",
      "logicalOperation": "AND"
    },
    {
      "id": "LeadType",
      "label": "Lead Type",
      "type": "string",
      "controlType": "dropdown",
      "value": "Commercial",
      "options": [],
      "operation": "Eq",
      "logicalOperation": "AND"
    },
    {
      "id": "Email",
      "label": "Email",
      "type": "string",
      "controlType": "text",
      "value": "gmail.com",
      "options": [],
      "operation": "Contains",
      "logicalOperation": "OR"
    },
    {
      "id": "Phone",
      "label": "Phone Number",
      "type": "string",
      "controlType": "text",
      "value": "555",
      "options": [],
      "operation": "Contains",
      "logicalOperation": "OR"
    },
    {
      "id": "Company",
      "label": "Company",
      "type": "string",
      "controlType": "text",
      "value": "Tech",
      "options": [],
      "operation": "Contains",
      "logicalOperation": "OR"
    },
    {
      "id": "City",
      "label": "City",
      "type": "string",
      "controlType": "text",
      "value": "New York",
      "options": [],
      "operation": "Contains",
      "logicalOperation": "OR"
    },
    {
      "id": "State",
      "label": "State",
      "type": "string",
      "controlType": "text",
      "value": "NY",
      "options": [],
      "operation": "Eq",
      "logicalOperation": "OR"
    },
    {
      "id": "EstimatedValueRange",
      "label": "Estimated Value Range",
      "type": "string",
      "controlType": "dropdown",
      "value": "10000-50000",
      "options": [],
      "operation": "Between",
      "logicalOperation": "AND"
    },
    {
      "id": "ProbabilityRange",
      "label": "Probability Range",
      "type": "string",
      "controlType": "dropdown",
      "value": "51-75",
      "options": [],
      "operation": "Between",
      "logicalOperation": "AND"
    },
    {
      "id": "DateOpenedRange",
      "label": "Date Opened Range",
      "type": "string",
      "controlType": "dropdown",
      "value": "last30Days",
      "options": [],
      "operation": "Between",
      "logicalOperation": "AND"
    },
    {
      "id": "IsActive",
      "label": "Is Active",
      "type": "boolean",
      "controlType": "radio",
      "value": "true",
      "options": [],
      "operation": "Eq",
      "logicalOperation": "AND"
    },
    {
      "id": "HasAttachments",
      "label": "Has Attachments",
      "type": "boolean",
      "controlType": "radio",
      "value": "true",
      "options": [],
      "operation": "Eq",
      "logicalOperation": "AND"
    },
    {
      "id": "AssignedTo",
      "label": "Assigned To",
      "type": "number",
      "controlType": "autocomplete",
      "value": "5",
      "options": [],
      "operation": "Eq",
      "logicalOperation": "AND"
    }
  ]
}

{
  "pageNumber": 1,
  "pageSize": 15,
  "sortBy": "LastName",
  "sortDirection": "Asc",
  "filters": [
    {
      "id": "FirstName",
      "value": "John",
      "operation": "Contains",
      "logicalOperation": "OR"
    },
    {
      "id": "Email",
      "value": "john",
      "operation": "Contains",
      "logicalOperation": "OR"
    }
  ]
}