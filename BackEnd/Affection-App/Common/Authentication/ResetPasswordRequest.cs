﻿
namespace Affection.Contract.Authentication;
public record ResetPasswordRequest
(
 string Email,
 string Code,
 string NewPassword
);