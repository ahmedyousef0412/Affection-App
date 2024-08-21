

namespace Affection.Contract.Authentication;
public record ConfirmEmailRequest
(
    string UserId,
    string Code
);
