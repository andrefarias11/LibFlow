﻿namespace LibFlow.Dto.Author;

public class UpdateAuthorDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
}
