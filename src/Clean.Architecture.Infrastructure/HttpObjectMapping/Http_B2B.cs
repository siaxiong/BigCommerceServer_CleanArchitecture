﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Infrastructure.HttpObjectMapping;

public record Http_B2B_CompanyUser_Payload(
  Http_B2B_CompanyUser[] data
  );
public record Http_B2B_CompanyUser (
  int id,
  string uuid ,
  int companyId,
  string email,
  string firstName,
  string lastName,
  string phoneNumber,
  int customerId
);
public record Http_B2B_Company_Payload (
  Http_B2B_Company data
  );

public record Http_B2B_Companies_Payload(
  List<Http_B2B_Company> data,
  Http_B2B_Resp_Metadata meta
  );
public record Http_B2B_Company(
  int companyId,
  string companyName,
  string bcGroupName,
  int companyStatus,
  string companyEmail,
  string companyPhone,
  string uuid
  );

public record Http_B2B_Resp_Metadata(
 Http_B2B_Resp_Pagination pagination
);

public record Http_B2B_Resp_Pagination(
  int limit,
  int offset,
  int totalCount
  );

