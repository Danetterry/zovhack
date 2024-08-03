using ClickableTransparentOverlay;
using ImGuiNET;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using zovhack;

namespace zovhack
{
    public class Renderer : Overlay
    {
        public Renderer() : base(screenWidth, screenHeight) { }

        public static void SetupImGuiStyle()
        {
            // Photoshop styleDerydoca from ImThemes
            var style = ImGuiNET.ImGui.GetStyle();

            style.Alpha = 1.0f;
            style.DisabledAlpha = 0.6000000238418579f;
            style.WindowPadding = new Vector2(8.0f, 8.0f);
            style.WindowRounding = 4.0f;
            style.WindowBorderSize = 1.0f;
            style.WindowMinSize = new Vector2(32.0f, 32.0f);
            style.WindowTitleAlign = new Vector2(0.0f, 0.5f);
            style.WindowMenuButtonPosition = ImGuiDir.Left;
            style.ChildRounding = 4.0f;
            style.ChildBorderSize = 1.0f;
            style.PopupRounding = 2.0f;
            style.PopupBorderSize = 1.0f;
            style.FramePadding = new Vector2(4.0f, 3.0f);
            style.FrameRounding = 2.0f;
            style.FrameBorderSize = 1.0f;
            style.ItemSpacing = new Vector2(8.0f, 4.0f);
            style.ItemInnerSpacing = new Vector2(4.0f, 4.0f);
            style.CellPadding = new Vector2(4.0f, 2.0f);
            style.IndentSpacing = 21.0f;
            style.ColumnsMinSpacing = 6.0f;
            style.ScrollbarSize = 13.0f;
            style.ScrollbarRounding = 12.0f;
            style.GrabMinSize = 7.0f;
            style.GrabRounding = 0.0f;
            style.TabRounding = 0.0f;
            style.TabBorderSize = 1.0f;
            style.TabMinWidthForCloseButton = 0.0f;
            style.ColorButtonPosition = ImGuiDir.Right;
            style.ButtonTextAlign = new Vector2(0.5f, 0.5f);
            style.SelectableTextAlign = new Vector2(0.0f, 0.0f);

            style.Colors[(int)ImGuiCol.Text] = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            style.Colors[(int)ImGuiCol.TextDisabled] = new Vector4(0.4980392158031464f, 0.4980392158031464f, 0.4980392158031464f, 1.0f);
            style.Colors[(int)ImGuiCol.WindowBg] = new Vector4(0.1764705926179886f, 0.1764705926179886f, 0.1764705926179886f, 1.0f);
            style.Colors[(int)ImGuiCol.ChildBg] = new Vector4(0.2784313857555389f, 0.2784313857555389f, 0.2784313857555389f, 0.0f);
            style.Colors[(int)ImGuiCol.PopupBg] = new Vector4(0.3098039329051971f, 0.3098039329051971f, 0.3098039329051971f, 1.0f);
            style.Colors[(int)ImGuiCol.Border] = new Vector4(0.2627451121807098f, 0.2627451121807098f, 0.2627451121807098f, 1.0f);
            style.Colors[(int)ImGuiCol.BorderShadow] = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
            style.Colors[(int)ImGuiCol.FrameBg] = new Vector4(0.1568627506494522f, 0.1568627506494522f, 0.1568627506494522f, 1.0f);
            style.Colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.2000000029802322f, 0.2000000029802322f, 0.2000000029802322f, 1.0f);
            style.Colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.2784313857555389f, 0.2784313857555389f, 0.2784313857555389f, 1.0f);
            style.Colors[(int)ImGuiCol.TitleBg] = new Vector4(0.1450980454683304f, 0.1450980454683304f, 0.1450980454683304f, 1.0f);
            style.Colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.1450980454683304f, 0.1450980454683304f, 0.1450980454683304f, 1.0f);
            style.Colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(0.1450980454683304f, 0.1450980454683304f, 0.1450980454683304f, 1.0f);
            style.Colors[(int)ImGuiCol.MenuBarBg] = new Vector4(0.1921568661928177f, 0.1921568661928177f, 0.1921568661928177f, 1.0f);
            style.Colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(0.1568627506494522f, 0.1568627506494522f, 0.1568627506494522f, 1.0f);
            style.Colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(0.2745098173618317f, 0.2745098173618317f, 0.2745098173618317f, 1.0f);
            style.Colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(0.2980392277240753f, 0.2980392277240753f, 0.2980392277240753f, 1.0f);
            style.Colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(1.0f, 0.3882353007793427f, 0.0f, 1.0f);
            style.Colors[(int)ImGuiCol.CheckMark] = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            style.Colors[(int)ImGuiCol.SliderGrab] = new Vector4(0.3882353007793427f, 0.3882353007793427f, 0.3882353007793427f, 1.0f);
            style.Colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(1.0f, 0.3882353007793427f, 0.0f, 1.0f);
            style.Colors[(int)ImGuiCol.Button] = new Vector4(1.0f, 1.0f, 1.0f, 0.0f);
            style.Colors[(int)ImGuiCol.ButtonHovered] = new Vector4(1.0f, 1.0f, 1.0f, 0.1560000032186508f);
            style.Colors[(int)ImGuiCol.ButtonActive] = new Vector4(1.0f, 1.0f, 1.0f, 0.3910000026226044f);
            style.Colors[(int)ImGuiCol.Header] = new Vector4(0.3098039329051971f, 0.3098039329051971f, 0.3098039329051971f, 1.0f);
            style.Colors[(int)ImGuiCol.HeaderHovered] = new Vector4(0.4666666686534882f, 0.4666666686534882f, 0.4666666686534882f, 1.0f);
            style.Colors[(int)ImGuiCol.HeaderActive] = new Vector4(0.4666666686534882f, 0.4666666686534882f, 0.4666666686534882f, 1.0f);
            style.Colors[(int)ImGuiCol.Separator] = new Vector4(0.2627451121807098f, 0.2627451121807098f, 0.2627451121807098f, 1.0f);
            style.Colors[(int)ImGuiCol.SeparatorHovered] = new Vector4(0.3882353007793427f, 0.3882353007793427f, 0.3882353007793427f, 1.0f);
            style.Colors[(int)ImGuiCol.SeparatorActive] = new Vector4(1.0f, 0.3882353007793427f, 0.0f, 1.0f);
            style.Colors[(int)ImGuiCol.ResizeGrip] = new Vector4(1.0f, 1.0f, 1.0f, 0.25f);
            style.Colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(1.0f, 1.0f, 1.0f, 0.6700000166893005f);
            style.Colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(1.0f, 0.3882353007793427f, 0.0f, 1.0f);
            style.Colors[(int)ImGuiCol.Tab] = new Vector4(0.09411764889955521f, 0.09411764889955521f, 0.09411764889955521f, 1.0f);
            style.Colors[(int)ImGuiCol.TabHovered] = new Vector4(0.3490196168422699f, 0.3490196168422699f, 0.3490196168422699f, 1.0f);
            style.Colors[(int)ImGuiCol.PlotLines] = new Vector4(0.4666666686534882f, 0.4666666686534882f, 0.4666666686534882f, 1.0f);
            style.Colors[(int)ImGuiCol.PlotLinesHovered] = new Vector4(1.0f, 0.3882353007793427f, 0.0f, 1.0f);
            style.Colors[(int)ImGuiCol.PlotHistogram] = new Vector4(0.5843137502670288f, 0.5843137502670288f, 0.5843137502670288f, 1.0f);
            style.Colors[(int)ImGuiCol.PlotHistogramHovered] = new Vector4(1.0f, 0.3882353007793427f, 0.0f, 1.0f);
            style.Colors[(int)ImGuiCol.TableHeaderBg] = new Vector4(0.1882352977991104f, 0.1882352977991104f, 0.2000000029802322f, 1.0f);
            style.Colors[(int)ImGuiCol.TableBorderStrong] = new Vector4(0.3098039329051971f, 0.3098039329051971f, 0.3490196168422699f, 1.0f);
            style.Colors[(int)ImGuiCol.TableBorderLight] = new Vector4(0.2274509817361832f, 0.2274509817361832f, 0.2470588237047195f, 1.0f);
            style.Colors[(int)ImGuiCol.TableRowBg] = new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
            style.Colors[(int)ImGuiCol.TableRowBgAlt] = new Vector4(1.0f, 1.0f, 1.0f, 0.05999999865889549f);
            style.Colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(1.0f, 1.0f, 1.0f, 0.1560000032186508f);
            style.Colors[(int)ImGuiCol.DragDropTarget] = new Vector4(1.0f, 0.3882353007793427f, 0.0f, 1.0f);
            style.Colors[(int)ImGuiCol.NavHighlight] = new Vector4(1.0f, 0.3882353007793427f, 0.0f, 1.0f);
            style.Colors[(int)ImGuiCol.NavWindowingHighlight] = new Vector4(1.0f, 0.3882353007793427f, 0.0f, 1.0f);
            style.Colors[(int)ImGuiCol.NavWindowingDimBg] = new Vector4(0.0f, 0.0f, 0.0f, 0.5860000252723694f);
            style.Colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(0.0f, 0.0f, 0.0f, 0.5860000252723694f);
        }

        private static int SM_CXSCREEN = 0;
        private static int SM_CYSCREEN = 1;
        private static int screenWidth = Renderer.GetSystemMetrics(Renderer.SM_CXSCREEN);
        private static int screenHeight = Renderer.GetSystemMetrics(Renderer.SM_CYSCREEN);
        public Vector2 screenSize = new Vector2((float)Renderer.screenWidth, (float)Renderer.screenHeight);
        public ConcurrentQueue<Entity> entities = new ConcurrentQueue<Entity>();
        private Entity localPlayer = new Entity();
        private readonly object entityLock = new object();
        public bool aimBot;
        public bool aimOnTeam;
        public bool showWindow = true;
        public bool enableBox;
        public bool cornerBox;
        public bool enableLine;
        public bool enableName;
        public bool enableBar;
        public bool enableWeapon;
        public bool enableOverlay;
        public bool enableBypass;
        public float boxRounding;
        public float hpRounding;
        public float boxThick;
        public float hpThick;
        public Vector4 enemyColor = new Vector4(1f, 0.0f, 0.0f, 1f);
        public Vector4 teamColor = new Vector4(0.0f, 1f, 0.0f, 1f);
        public Vector4 teamNameColor = new Vector4(1f, 1f, 1f, 1f);
        public Vector4 enemyNameColor = new Vector4(1f, 1f, 1f, 1f);
        public Vector4 teamHealthColor = new Vector4(0.0f, 1f, 0.0f, 1f);
        public Vector4 enemyHealthColor = new Vector4(0.0f, 1f, 0.0f, 1f);
        public Vector4 teamWeaponColor = new Vector4(1f, 1f, 1f, 1f);
        public Vector4 enemyWeaponColor = new Vector4(1f, 1f, 1f, 1f);
        public Vector4 enemyLineColor = new Vector4(1f, 0.0f, 0.0f, 1f);
        public Vector4 teamLineColor = new Vector4(0.0f, 1f, 0.0f, 1f);
        private string fontPath = "C:\\Windows\\Fonts\\verdana.ttf";
        public string configField = "config";
        private ImDrawListPtr drawList;

        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int nIndex);

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        protected override void Render()
        {
            SetupImGuiStyle();
            ImGuiStylePtr style = ImGui.GetStyle();
            style.WindowRounding = 2f;
            style.ChildRounding = 2f;
            style.FrameRounding = 2f;
            style.PopupRounding = 2f;
            style.ScrollbarRounding = 4f;
            style.GrabRounding = 2f;
            style.TabRounding = 4f;
            this.ReplaceFont(this.fontPath, 16, FontGlyphRangeType.Cyrillic);
            if (this.enableOverlay)
            {
                ImGui.SetNextWindowBgAlpha(0.7f);
                ImGui.SetNextWindowPos(new Vector2(10f, 10f));
                ImGui.Begin("overlay", ImGuiWindowFlags.NoDecoration | ImGuiWindowFlags.NoInputs | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.AlwaysAutoResize | ImGuiWindowFlags.NoBringToFrontOnFocus);
                ImGui.Text("ZovHack v0.29");
                ImGui.Text("Compilation Date: 6/29/2024");
                ImGui.Separator();
                ImGui.Text("Current Date: " + DateTime.Now.ToString());
            }
            if (Renderer.GetAsyncKeyState(45) < (short)0)
            {
                this.showWindow = !this.showWindow;
                Thread.Sleep(200);
            }
            if (this.showWindow)
            {
                ImGui.Begin("ZovHack v1.0");
                if (ImGui.BeginTabBar("Tabs"))
                {
                    if (ImGui.BeginTabItem("Main"))
                    {
                        ImGui.Text("Welcome to ZovHack - External CS2 cheat.");
                        ImGui.Text("Maked by okt, updated by Danetterry");
                        ImGui.TextDisabled("To hide/open the menu press 'Insert'");
                        ImGui.TextDisabled("Нехуй время терять надо панчи искать");
                        ImGui.EndTabItem();
                    }
                    if (ImGui.BeginTabItem("Aimbot (broken)"))
                    {
                        ImGui.Checkbox("Enable", ref this.aimBot);
                        ImGui.Checkbox("Aim on team", ref this.aimOnTeam);
                        ImGui.EndTabItem();
                    }
                    if (ImGui.BeginTabItem("Visuals"))
                    {
                        ImGui.Checkbox("Box [...]", ref this.enableBox);
                        ImGui.SameLine();
                        if (ImGui.BeginPopupContextItem("##Box"))
                        {
                            ImGui.Checkbox("Corner", ref this.cornerBox);
                            ImGui.EndPopup();
                        }
                        ImGui.SameLine(ImGui.GetWindowWidth() - 65f);
                        ImGui.ColorEdit4("Box Enemy Color", ref this.enemyColor, ImGuiColorEditFlags.NoInputs | ImGuiColorEditFlags.NoLabel);
                        ImGui.SameLine();
                        ImGui.ColorEdit4("Box Team Color", ref this.teamColor, ImGuiColorEditFlags.NoInputs | ImGuiColorEditFlags.NoLabel);
                        ImGui.Checkbox("Line", ref this.enableLine);
                        ImGui.SameLine(ImGui.GetWindowWidth() - 65f);
                        ImGui.ColorEdit4("Line Enemy Color", ref this.enemyLineColor, ImGuiColorEditFlags.NoInputs | ImGuiColorEditFlags.NoLabel);
                        ImGui.SameLine();
                        ImGui.ColorEdit4("Line Team Color", ref this.teamLineColor, ImGuiColorEditFlags.NoInputs | ImGuiColorEditFlags.NoLabel);
                        ImGui.Checkbox("HP Bar", ref this.enableBar);
                        ImGui.SameLine(ImGui.GetWindowWidth() - 65f);
                        ImGui.ColorEdit4("HP Enemy Color", ref this.enemyHealthColor, ImGuiColorEditFlags.NoInputs | ImGuiColorEditFlags.NoLabel);
                        ImGui.SameLine();
                        ImGui.ColorEdit4("HP Team Color", ref this.teamHealthColor, ImGuiColorEditFlags.NoInputs | ImGuiColorEditFlags.NoLabel);
                        ImGui.Checkbox("Name", ref this.enableName);
                        ImGui.SameLine();
                        ImGui.SameLine(ImGui.GetWindowWidth() - 65f);
                        ImGui.ColorEdit4("Name Enemy Color", ref this.enemyNameColor, ImGuiColorEditFlags.NoInputs | ImGuiColorEditFlags.NoLabel);
                        ImGui.SameLine();
                        ImGui.ColorEdit4("Name Team Color", ref this.teamNameColor, ImGuiColorEditFlags.NoInputs | ImGuiColorEditFlags.NoLabel);
                        ImGui.Checkbox("Weapon", ref this.enableWeapon);
                        ImGui.SameLine();
                        ImGui.SameLine(ImGui.GetWindowWidth() - 65f);
                        ImGui.ColorEdit4("Weapon Enemy Color", ref this.enemyWeaponColor, ImGuiColorEditFlags.NoInputs | ImGuiColorEditFlags.NoLabel);
                        ImGui.SameLine();
                        ImGui.ColorEdit4("Weapon Team Color", ref this.teamWeaponColor, ImGuiColorEditFlags.NoInputs | ImGuiColorEditFlags.NoLabel);
                        ImGui.EndTabItem();
                    }
                    if (ImGui.BeginTabItem("Misc"))
                    {
                        ImGui.Text("Rounding");

                        ImGui.SliderFloat("Box", ref this.boxRounding, 0.0f, 10f);
                        ImGui.SliderFloat("HP Bar", ref this.hpRounding, 0.0f, 10f);

                        ImGui.Separator();

                        ImGui.Checkbox("Overlay", ref this.enableOverlay);

                        ImGui.Separator();

                        if (ImGui.Button("Load Config"))
                        {
                            Config config = new Config();
                            Parameters parameters = config.Load(configField);
                            aimBot = parameters.aimBot;
                            aimOnTeam = parameters.aimOnTeam;
                            showWindow = parameters.showWindow;
                            enableBox = parameters.enableBox;
                            cornerBox = parameters.cornerBox;
                            enableLine = parameters.enableLine;
                            enableName = parameters.enableName;
                            enableBar = parameters.enableBar;
                            enableWeapon = parameters.enableWeapon;
                            enableOverlay = parameters.enableOverlay;
                            enableBypass = parameters.enableBypass;
                            boxRounding = parameters.boxRounding;
                            hpRounding = parameters.hpRounding;
                            boxThick = parameters.boxThick;
                            hpThick = parameters.hpThick;
                            enemyColor = parameters.enemyColor;
                            teamColor = parameters.teamColor;
                            teamNameColor = parameters.teamNameColor;
                            enemyNameColor = parameters.enemyNameColor;
                            teamHealthColor = parameters.teamHealthColor;
                            enemyHealthColor = parameters.enemyHealthColor;
                            teamWeaponColor = parameters.teamWeaponColor;
                            enemyWeaponColor = parameters.enemyWeaponColor;
                            enemyLineColor = parameters.enemyLineColor;
                            teamLineColor = parameters.teamLineColor;
                        }

                        ImGui.SameLine();

                        if (ImGui.Button("Save Config"))
                        {
                            Parameters parameters = new Parameters();
                            parameters.aimBot = aimBot;
                            parameters.aimOnTeam = aimOnTeam;
                            parameters.showWindow = showWindow;
                            parameters.enableBox = enableBox;
                            parameters.cornerBox = cornerBox;
                            parameters.enableLine = enableLine;
                            parameters.enableName = enableName;
                            parameters.enableBar = enableBar;
                            parameters.enableWeapon = enableWeapon;
                            parameters.enableOverlay = enableOverlay;
                            parameters.enableBypass = enableBypass;
                            parameters.boxRounding = boxRounding;
                            parameters.hpRounding = hpRounding;
                            parameters.boxThick = boxThick;
                            parameters.hpThick = hpThick;
                            parameters.enemyColor = enemyColor;
                            parameters.teamColor = teamColor;
                            parameters.teamNameColor = teamNameColor;
                            parameters.enemyNameColor = enemyNameColor;
                            parameters.teamHealthColor = teamHealthColor;
                            parameters.enemyHealthColor = enemyHealthColor;
                            parameters.teamWeaponColor = teamWeaponColor;
                            parameters.enemyWeaponColor = enemyWeaponColor;
                            parameters.enemyLineColor = enemyLineColor;
                            parameters.teamLineColor = teamLineColor;
                            Config config = new Config();
                            config.Save(parameters, configField);
                        }

                        ImGui.SameLine();

                        ImGui.InputText("##input", ref configField, 32);

                        ImGui.SameLine();

                        if (ImGui.Button("Config List"))
                            ImGui.OpenPopup("ConfigPopup");

                        if (ImGui.BeginPopup("ConfigPopup"))
                        {
                            Config config = new Config();
                            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                            var fullPath = Path.Combine(documents, "ZovHack\\");
                            string[] items = config.GetFilesAndFolders(fullPath);

                            ImGui.Text("cfgs in documents folder");
                            ImGui.Separator();

                            for (int i = 0; i < items.Length; i++)
                            {
                                if (ImGui.Selectable(items[i], items[i] == configField))
                                {
                                    configField = items[i];
                                    ImGui.CloseCurrentPopup();
                                }
                            }

                            ImGui.EndPopup();
                        }

                        ImGui.Separator();

                        if (ImGui.Button("Unhook"))
                            Environment.Exit(0);

                        ImGui.EndTabItem();
                    }
                    ImGui.EndTabBar();
                }
            }
            this.DrawOverlay(this.screenSize);
            this.drawList = ImGui.GetWindowDrawList();
            foreach (Entity entity in this.entities)
            {
                if (this.enableWeapon)
                    this.DrawWeapons(entity);
                if (this.enableName)
                    this.DrawName(entity, 16);
                if (this.enableBox)
                {
                    if (this.cornerBox)
                        this.DrawCornerBox(entity);
                    else
                        this.DrawBox(entity);
                }
                if (this.enableBar && EntityOnScreen(entity))
                    this.DrawHealth(entity);
                if (this.enableLine)
                    this.DrawLine(entity);
            }
            ImGui.End();

            bool EntityOnScreen(Entity entity)
            {
                return (double)entity.position2D.X > 0.0 && (double)entity.position2D.Y < (double)this.screenSize.X && (double)entity.position2D.Y > 0.0 && (double)entity.position2D.Y < (double)this.screenSize.Y;
            }
        }

        private void DrawName(Entity entity, int yOffset)
        {
            Vector2 vector2 = ImGui.CalcTextSize(entity.name);
            this.drawList.AddText(new Vector2(entity.viewPosition2D.X - vector2.X / 2f, entity.viewPosition2D.Y - (float)yOffset), ImGui.ColorConvertFloat4ToU32(this.localPlayer.team == entity.team ? this.teamNameColor : this.enemyNameColor), entity.name ?? "");
        }

        private void DrawWeapons(Entity entity)
        {
            Vector2 vector2 = ImGui.CalcTextSize(entity.currentWeaponName);
            this.drawList.AddText(new Vector2(entity.viewPosition2D.X - vector2.X / 2f, entity.position2D.Y), ImGui.ColorConvertFloat4ToU32(this.localPlayer.team == entity.team ? this.teamWeaponColor : this.enemyWeaponColor), entity.currentWeaponName ?? "");
        }

        private void DrawBox(Entity entity)
        {
            float num = entity.position2D.Y - entity.viewPosition2D.Y;
            this.drawList.AddRect(new Vector2(entity.viewPosition2D.X - num / 3f, entity.viewPosition2D.Y), new Vector2(entity.position2D.X + num / 3f, entity.viewPosition2D.Y + num), ImGui.ColorConvertFloat4ToU32(this.localPlayer.team == entity.team ? this.teamColor : this.enemyColor), this.boxRounding);
        }

        private void DrawCornerBox(Entity entity)
        {
            float num1 = entity.position2D.Y - entity.viewPosition2D.Y;
            Vector2 vector2_1 = new Vector2(entity.viewPosition2D.X - num1 / 3f, entity.viewPosition2D.Y);
            Vector2 vector2_2 = new Vector2(entity.position2D.X + num1 / 3f, entity.viewPosition2D.Y + num1);
            Vector4 @in = this.localPlayer.team == entity.team ? this.teamColor : this.enemyColor;
            float x1 = vector2_1.X;
            float x2 = vector2_2.X;
            float y1 = vector2_1.Y;
            float y2 = vector2_2.Y;
            float num2 = (float)Math.Floor((double)Math.Abs(x1 - x2) / 4.0);
            float num3 = (float)Math.Floor((double)Math.Abs(y1 - y2) / 4.0);
            this.drawList.AddLine(new Vector2(x1 - 0.5f, y2), new Vector2((float)((double)x1 + (double)num2 + 0.5), y2), ImGui.ColorConvertFloat4ToU32(@in), this.boxThick);
            this.drawList.AddLine(new Vector2((float)((double)x2 - (double)num2 - 0.5), y2), new Vector2(x2 + 0.5f, y2), ImGui.ColorConvertFloat4ToU32(@in), this.boxThick);
            this.drawList.AddLine(new Vector2((float)((double)x1 + (double)num2 + 0.5), y1), new Vector2(x1 - 0.5f, y1), ImGui.ColorConvertFloat4ToU32(@in), this.boxThick);
            this.drawList.AddLine(new Vector2(x2 + 0.5f, y1), new Vector2((float)((double)x2 - (double)num2 - 0.5), y1), ImGui.ColorConvertFloat4ToU32(@in), this.boxThick);
            this.drawList.AddLine(new Vector2(x1, y2 + 0.5f), new Vector2(x1, (float)((double)y2 - (double)num3 - 0.5)), ImGui.ColorConvertFloat4ToU32(@in), this.boxThick);
            this.drawList.AddLine(new Vector2(x1, (float)((double)y1 + (double)num3 + 0.5)), new Vector2(x1, y1 - 0.5f), ImGui.ColorConvertFloat4ToU32(@in), this.boxThick);
            this.drawList.AddLine(new Vector2(x2, y1 - 0.5f), new Vector2(x2, (float)((double)y1 + (double)num3 + 0.5)), ImGui.ColorConvertFloat4ToU32(@in), this.boxThick);
            this.drawList.AddLine(new Vector2(x2, (float)((double)y2 - (double)num3 - 0.5)), new Vector2(x2, y2 + 0.5f), ImGui.ColorConvertFloat4ToU32(@in), this.boxThick);
        }

        private void DrawLine(Entity entity)
        {
            if ((double)entity.position2D.X == -99.0 || (double)entity.position2D.Y == -99.0)
                return;
            Vector4 @in = this.localPlayer.team == entity.team ? this.teamLineColor : this.enemyLineColor;
            this.drawList.AddLine(new Vector2(this.screenSize.X / 2f, this.screenSize.Y / 2f), entity.position2D, ImGui.ColorConvertFloat4ToU32(@in));
            if (!(this.cornerBox | !this.enableBox))
                return;
            this.drawList.AddCircle(entity.position2D, 2f, ImGui.ColorConvertFloat4ToU32(@in));
        }

        private void DrawHealth(Entity entity)
        {
            float num1 = entity.position2D.Y - entity.viewPosition2D.Y;
            float x = entity.viewPosition2D.X - num1 / 3f;
            float num2 = (float)(0.079999998211860657 * ((double)(entity.position2D.X + num1 / 3f) - (double)x));
            float num3 = num1 * ((float)entity.health / 100f);
            this.drawList.AddRectFilled(new Vector2(x - num2, entity.position2D.Y - num3), new Vector2(x, entity.position2D.Y), ImGui.ColorConvertFloat4ToU32(this.localPlayer.team == entity.team ? this.teamHealthColor : this.enemyHealthColor), this.hpRounding);
        }

        public void UpdateEntities(IEnumerable<Entity> newEntities)
        {
            this.entities = new ConcurrentQueue<Entity>(newEntities);
        }

        public Entity GetLocalPlayer()
        {
            lock (this.entityLock)
                return this.localPlayer;
        }

        public void UpdateLocalPlayer(Entity newEntity)
        {
            lock (this.entityLock)
                this.localPlayer = newEntity;
        }

        private void DrawOverlay(Vector2 screensize)
        {
            ImGui.SetNextWindowSize(screensize);
            ImGui.SetNextWindowPos(new Vector2(0.0f, 0.0f));
            ImGui.Begin("espoverlay", ImGuiWindowFlags.NoDecoration | ImGuiWindowFlags.NoScrollWithMouse | ImGuiWindowFlags.NoBackground | ImGuiWindowFlags.NoBringToFrontOnFocus | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoInputs);
        }
    }
}
