﻿namespace WaveFunctionCollapse.Possibilities;

public interface IPossibility
{
    public string Name { get; }
    public string GetText();

    public bool IsPossible(ICellContext cellContext);
}