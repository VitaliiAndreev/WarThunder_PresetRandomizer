// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.

[
    assembly: System.Diagnostics.CodeAnalysis.SuppressMessage
    (
        "Style",
        "IDE0059:Value assigned to symbol is never used",
        Justification = "Maintaining code style, since this warning now seems to pop up if the value is never used in at least one of the branching paths, like when throwing an exception.",
        Scope = "member",
        Target = "~M:Core.Json.Helpers.JsonHelper.DeserializeList``1(System.String)~System.Collections.Generic.Dictionary{System.String,``0}"
    )
]

