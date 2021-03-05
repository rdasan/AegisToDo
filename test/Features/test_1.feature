Feature: Customer Created Investor

  Support Creating Investors in CRM on the fly. These investors would automatically create Rolodex firms.
  A sell-side user goes to create an order for an investor in the order book. They look up the investor by name and see results returned from their CRM database on CMG. They determine that the investor does not yet exist in the CRM db, and the UI and API provides them a way to go ahead and create the investor firm. 
  They populate certain identifying information for that firm, at minimum name is required, but they have options to include BankCrmFirmKey, Website, address, other identifiers, etc. Once they have created that CRM firm, it can be used to submit orders similar to existing CRM firms.

  @ECM-6846 @COMPLETED
  Scenario: Create Customer Created Investor with Firm Name
    Given Firm Name 'Investor X'
    When Syndicate user creates new Crm Investor firm
    Then Crm Firm is created with valid CmgEntityKey and IsCustomerCreated set to true and other validations
    And Rolodex Firm is created with Firm Name and IsCustomerCreated set to true
    
    
Feature: My Feature

  @ECM-6846 @COMPLETED
  Scenario: Hello
    Given This is s given
    When My When
    Then MyThen
    
Feature: Second Feature

  @ECM-6456 @WIP
  Scenario: Second Hello
  Given: Secong Hello given

