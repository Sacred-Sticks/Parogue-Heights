namespace Parogue_Heights
{
    public static class ToolFactory
    {
        public static ITool CreateTool(Tool toolData)
        {
            return toolData.ItemName switch
            {
                "Trampoline" => new Trampoline(),
                _ => default
            };
        }
    }
}
