
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Luban;
using SimpleJSON;

namespace cfg
{
public partial class Tables
{
    public item.TbItem TbItem {get; }

    public Tables(System.Func<string, JSONNode> loader)
    {
        TbItem = new item.TbItem(loader("item_tbitem"));
        ResolveRef();
    }
    
    private void ResolveRef()
    {
        TbItem.ResolveRef(this);
    }
}

}
