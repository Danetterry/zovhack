using Swed64;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;
using zovhack;


Swed swed = new Swed("cs2");
IntPtr moduleBase = swed.GetModuleBase("client.dll");
Renderer renderer = new Renderer();
renderer.Start().Wait();
Vector2 screenSize = renderer.screenSize;
List<Entity> entityList = new List<Entity>();
Entity newEntity = new Entity();
while (true)
{
    entityList.Clear();
    IntPtr addy1 = swed.ReadPointer(moduleBase, Offsets.dwEntityList);
    IntPtr addy2 = swed.ReadPointer(addy1, 16);
    IntPtr address1 = swed.ReadPointer(moduleBase, Offsets.dwLocalPlayerPawn);
    newEntity.origin = swed.ReadVec(address1, Offsets.m_vOldOrigin);
    newEntity.view = swed.ReadVec(address1, Offsets.m_vecViewOffset);
    newEntity.team = swed.ReadInt(address1, Offsets.m_iTeamNum);
    for (int index = 0; index < 64; ++index)
    {
        if (addy2 != IntPtr.Zero)
        {
            IntPtr address2 = swed.ReadPointer(addy2, index * 120);
            if (address2 != IntPtr.Zero)
            {
                int num1 = swed.ReadInt(address2, Offsets.m_hPlayerPawn);
                if (num1 != 0)
                {
                    IntPtr addy3 = swed.ReadPointer(addy1, 8 * ((num1 & (int)short.MaxValue) >> 9) + 16);
                    if (addy3 != IntPtr.Zero)
                    {
                        IntPtr num2 = swed.ReadPointer(addy3, 120 * (num1 & 511));
                        if (num2 != address1)
                        {
                            IntPtr address3 = swed.ReadPointer(num2, Offsets.m_pClippingWeapon);
                            short num3 = swed.ReadShort(address3, Offsets.m_AttributeManager + Offsets.m_Item + Offsets.m_iItemDefinitionIndex);
                            if (num3 != (short)-1)
                            {
                                float[] matrix = swed.ReadMatrix(moduleBase + (IntPtr)Offsets.dwViewMatrix);
                                Entity entity = new Entity()
                                {
                                    name = swed.ReadString(address2, Offsets.m_iszPlayerName, 16).Split("\0")[0],
                                    health = swed.ReadInt(num2, Offsets.m_iHealth),
                                    team = swed.ReadInt(num2, Offsets.m_iTeamNum),
                                    position = swed.ReadVec(num2, Offsets.m_vOldOrigin),
                                    viewOffset = swed.ReadVec(num2, Offsets.m_vecViewOffset),
                                    currentWeaponName = Enum.GetName(typeof(Weapon), (object)num3),
                                    view = swed.ReadVec(num2, Offsets.m_vecViewOffset)
                                };
                                entity.distance = Vector3.Distance(entity.origin, newEntity.origin);
                                if (swed.ReadInt(num2, Offsets.m_lifeState) == 256 && (entity.team != newEntity.team || renderer.aimOnTeam))
                                {
                                    entity.position2D = Calculate.WorldToScreen(matrix, entity.position, screenSize);
                                    entity.viewPosition2D = Calculate.WorldToScreen(matrix, Vector3.Add(entity.position, entity.viewOffset), screenSize);
                                    if (num2 != address1)
                                        entityList.Add(entity);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    entityList = Enumerable.ToList<Entity>(Enumerable.OrderBy<Entity, float>(entityList, (Func<Entity, float>)(o => o.distance)));
    if (entityList.Count > 0 && GetAsyncKeyState(164) < (short)0 && renderer.aimBot)
    {
        Vector2 angles = CalculateAim.CalculateAngles(Vector3.Add(newEntity.origin, newEntity.view), Vector3.Add(entityList[0].origin, entityList[0].view));
        Vector3 vector3 = new Vector3(angles.Y, angles.X, 0.0f);
        swed.WriteVec(moduleBase, Offsets.dwEntityList, vector3);
    }
    Thread.Sleep(16);
    renderer.UpdateLocalPlayer(newEntity);
    renderer.UpdateEntities((IEnumerable<Entity>)entityList);
}

[DllImport("user32.dll")]
static extern short GetAsyncKeyState(int vKey);
