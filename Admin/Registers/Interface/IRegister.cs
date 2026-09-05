namespace Admin.Registers.Interface
{
    /// <summary>
    /// Base marker interface. Every registrar (builder-time or pipeline-time)
    /// implements one of the interfaces below, which in turn implement this one.
    /// This lets RegisterExtensions.GetRegistrars&lt;T&gt;() constrain its generic
    /// parameter to "any kind of registrar" while still being type-safe.
    /// </summary>
    public interface IRegister
    {
    }


}
