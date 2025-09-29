namespace api.Features.Users.Domain;

using System;

public record User(Guid Id, String Name, String Username, String Email);
