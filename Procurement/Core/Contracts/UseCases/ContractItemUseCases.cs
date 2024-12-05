﻿/* Empiria Operations ****************************************************************************************
*                                                                                                            *
*  Module   : Contracts Management                       Component : Use cases Layer                         *
*  Assembly : Empiria.Procurement.Core.dll               Pattern   : Use case interactor class               *
*  Type     : ContractItemUseCases                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases for contract items management.                                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.Services;

using Empiria.Procurement.Contracts.Adapters;

namespace Empiria.Procurement.Contracts.UseCases {

  /// <summary>Use cases for contract items management.</summary>
  public class ContractItemUseCases : UseCase {

    #region Constructors and parsers

    protected ContractItemUseCases() {
      // no-op
    }

    static public ContractItemUseCases UseCaseInteractor() {
      return UseCase.CreateInstance<ContractItemUseCases>();
    }

    #endregion Constructors and parsers

    #region Use cases

    public ContractItemDto AddContractItem(string contractUID, ContractItemFields fields) {
      Assertion.Require(contractUID, nameof(contractUID));
      Assertion.Require(fields, nameof(fields));

      fields.EnsureValid();

      var contract = Contract.Parse(contractUID);

      ContractItem contractItem = contract.AddItem(fields);

      contractItem.Save();

      return ContractItemMapper.Map(contractItem);
    }


    public ContractItemDto GetContractItem(string contractItemUID) {

      Assertion.Require(contractItemUID, nameof(contractItemUID));

      var contractItem = ContractItem.Parse(contractItemUID);

      return ContractItemMapper.Map(contractItem);
    }


    public ContractItemDto RemoveContractItem(string contractUID, string contractItemUID) {
      Assertion.Require(contractUID, nameof(contractUID));
      Assertion.Require(contractItemUID, nameof(contractItemUID));

      var contract = Contract.Parse(contractUID);

      ContractItem contractItem = contract.RemoveItem(contractItemUID);

      contractItem.Save();

      return ContractItemMapper.Map(contractItem);
    }


    public ContractItemDto UpdateContractItem(string contractUID,
                                              string contractItemUID,
                                              ContractItemFields fields) {
      Assertion.Require(contractUID, nameof(contractUID));
      Assertion.Require(contractItemUID, nameof(contractItemUID));
      Assertion.Require(fields, nameof(fields));

      fields.EnsureValid();

      var contract = Contract.Parse(contractUID);

      ContractItem contractItem = contract.GetItem(contractItemUID);

      contract.UpdateItem(contractItem, fields);

      contractItem.Save();

      return ContractItemMapper.Map(contractItem);
    }

    #endregion Use cases

  }  // class ContractUseCases

}  // namespace Empiria.Procurement.Contracts.UseCases
