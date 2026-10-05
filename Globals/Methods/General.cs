using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using YogsothothsYardMod.Globals.Instances.Items;
using YogsothothsYardMod.Globals.Instances.Projs;
using YogsothothsYardMod.Globals.Players;

namespace YogsothothsYardMod.Globals.Methods
{
    public static partial class YardMethods
    {
        public static YardPlayer YardMod(this Player player) => player.GetModPlayer<YardPlayer>();
        public static YardGlobalItem YardMod(this Item item) => item.GetGlobalItem<YardGlobalItem>();
        public static YardGlobalProjs YardMod(this Projectile proj) => proj.GetGlobalProjectile<YardGlobalProjs>();
        public static void SetUpHeldProj(this Projectile proj, int eu = 0)
        {
            proj.ignoreWater = true;
            proj.tileCollide = false;
            proj.usesLocalNPCImmunity = true;
            proj.penetrate = -1;
            proj.extraUpdates = eu;
            proj.noEnchantmentVisuals = true;
            proj.timeLeft = 10000;
        }
        public static Vector2 GetNormalVector2(this Vector2 beginPos, Vector2 endPos, Vector2? normalvalue = null) => (endPos - beginPos).ToSafeNormalize(normalvalue);
        public static Vector2 ToRandVelocity(this Vector2 srcVel, float randRads, float speed = 1f) => srcVel.ToSafeNormalize().RotatedBy(Main.rand.NextFloat(randRads) * Main.rand.NextBool().ToDirectionInt()) * speed;
        public static Vector2 ToRandVelocity(this Vector2 srcVel, float randRads, float minSpeed, float maxSpeed)
        {
            return srcVel.ToRandVelocity(randRads, Main.rand.NextFloat(minSpeed, maxSpeed));
        }
        public static Vector2 ToSafeNormalize(this Vector2 srcVel, Vector2? what = null) => srcVel.SafeNormalize(what ?? Vector2.UnitX);

        public static string ToPercent(this float value)
        {
            float value2 = value * 100f;
            return $"{(int)value2}%";
        }
        public static string ToLifeRegenFormat(this int value)
        {
            return $"+{(value / 2)} HP/s";
        }
        public static Vector2 ToMouseVector2(this Vector2 center, Vector2? safeValue = null)
        {
            Vector2 safe = safeValue ?? Vector2.UnitX;
            return (Main.MouseWorld - center).SafeNormalize(safe);

        }
        public static bool IsItemName(this DrawableTooltipLine line) => line.Name == "ItemName" && line.Mod == "Terraria";
        /// <summary>
        /// 控制玩家的手臂旋转，可选择前臂、后臂或双臂。
        /// </summary>
        /// <param name="player">目标玩家。</param>
        /// <param name="armRot">手臂的目标旋转角度（世界坐标系下的弧度，通常为武器/物品的方向）。</param>
        /// <param name="specificArm">
        /// 指定要控制的手臂：<br/>
        /// -  <c>0</c>（默认）：同时控制前臂和后臂。<br/>
        /// -  <c>&gt;0</c>（正数）：仅控制前臂（CompositeArmFront）。<br/>
        /// -  <c>&lt;0</c>（负数）：仅控制后臂（CompositeArmBack）。
        /// </param>
        /// <param name="customArmRot">
        /// 手臂的本地基础偏移角度（弧度），用于调整手臂默认朝向。实际最终旋转角度 = <paramref name="armRot"/> - <paramref name="customArmRot"/>。<br/>
        /// 默认值为 <see cref="PiOver2"/>（90°），表示手臂默认指向正右方时，需要减去此偏移以获得正确的贴图旋转。
        /// </param>
        public static void ControlPlayerArm(this Player player, float armRot, int specificArm = 0, float customArmRot = PiOver2)
        {
            float armType = Math.Sign(specificArm);
            switch (armType)
            {
                case 1:
                    player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, armRot - customArmRot);
                    break;
                case -1:
                    player.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, armRot - customArmRot);
                    break;
                default:
                    player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, armRot - customArmRot);
                    player.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, armRot - customArmRot);
                    break;
            }
        }
        public static bool HasProj<T>(this Player player) where T : ModProjectile => HasProj(player, ProjectileType<T>());
        public static bool HasProj(this Player player, int projID) => player.ownedProjectileCounts[projID] > 0;

        /// <summary>
        /// 重载一个out传参，输出你判定的拥有的proj的ID以方便后续可能需要的计算，或者别的
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="player"></param>
        /// <param name="ProjID"></param>
        /// <returns></returns>
        public static bool HasProj<T>(this Player player, out int ProjID) where T : ModProjectile
        {
            ProjID = ProjectileType<T>();
            return HasProj<T>(player);
        }
        public static bool OutOffScreen(Vector2 pos)
        {
            if (pos.X < Main.screenPosition.X - Main.screenWidth / 2)
                return true;

            if (pos.Y < Main.screenPosition.Y - Main.screenHeight / 2)
                return true;

            if (pos.X > Main.screenPosition.X + Main.screenWidth * 1.5f)
                return true;
            if (pos.Y > Main.screenPosition.Y + Main.screenHeight * 1.5f)
                return true;

            return false;
        }
        public static bool OutOffScreen(Vector2 pos, float areamult = 1f)
        {
            float halfwidth = Main.screenWidth / 2;
            float halfheight = Main.screenHeight / 2;
            if (pos.X < Main.screenPosition.X - halfwidth * areamult)
                return true;

            if (pos.Y < Main.screenPosition.Y - halfheight * areamult)
                return true;

            if (pos.X > Main.screenPosition.X + Main.screenWidth + halfwidth * areamult)
                return true;
            if (pos.Y > Main.screenPosition.Y + Main.screenHeight + halfheight * areamult)
                return true;

            return false;
        }
        /// <summary>
        /// 用于手持弹幕，获取斜45°近战武器的挥舞
        /// </summary>
        public static void GetProjDrawInfo_Melee(this Projectile proj, out Texture2D texture, out Vector2 drawPosition, out float drawRotation, out Vector2 rotationPoint, out SpriteEffects flipSprite)
        {
            texture = TextureAssets.Projectile[proj.type].Value;
            drawPosition = proj.Center - Main.screenPosition;
            drawRotation = proj.rotation + (proj.spriteDirection == -1 ? PiOver2 + PiOver4 : PiOver4);
            rotationPoint = proj.spriteDirection == -1 ? new Vector2(texture.Width, texture.Height) : new Vector2(0, texture.Height);
            flipSprite = proj.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        }
        public static void FastDraw(this SpriteBatch sb, Texture2D tex, Vector2 pos, Color c, float rotation, Vector2 origin, float scale, SpriteEffects se, int wtfisthis = 0)
        {
            sb.Draw(tex, pos, null, c, rotation, origin, scale, se, wtfisthis);
        }
        public static void FastDraw(this SpriteBatch sb, Texture2D tex, Vector2 pos, Color c, float rotation, Vector2 origin, Vector2 scale, SpriteEffects se, int wtfisthis = 0)
        {
            sb.Draw(tex, pos, null, c, rotation, origin, scale, se, wtfisthis);
        }
        public static Vector2 GetToMouseVector2(this Player player, Vector2 BeginPos)
        {
            Vector2 vector = Main.MouseWorld - BeginPos;
            vector = vector.SafeNormalize(Vector2.UnitX);
            return vector;
        }
        /// <summary>
        /// 轨迹设置，一般情况下默认使用模式2
        /// </summary>
        /// <param name="proj"></param>
        /// <param name="length"></param>
        /// <param name="mode"></param>
        public static void ToTrailSetting(this Projectile proj, int length = 4, int mode = 2)
        {
            ProjectileID.Sets.TrailingMode[proj.type] = mode;
            ProjectileID.Sets.TrailCacheLength[proj.type] = length;
        }
        public static Texture2D GetTexture(this Projectile proj) => TextureAssets.Projectile[proj.type].Value;
        public static Vector2 SafeDir(this Projectile proj) => proj.velocity.ToSafeNormalize();
        public static Vector2 SafeDirByRot(this Projectile proj) => proj.rotation.ToRotationVector2();
        public static bool MinionAntiClump(this Projectile proj, float pushForce = 0.05f)
        {
            for (int k = 0; k < Main.maxProjectiles; k++)
            {
                Projectile otherProj = Main.projectile[k];
                if (!otherProj.active || otherProj.owner != proj.owner || k == proj.whoAmI)
                    continue;

                bool sameProjType = otherProj.type == proj.type;
                float taxicabDist = Math.Abs(proj.position.X - otherProj.position.X) + Math.Abs(proj.position.Y - otherProj.position.Y);
                if (sameProjType && taxicabDist < proj.width)
                {
                    if (proj.position.X < otherProj.position.X)
                        proj.velocity.X -= pushForce;
                    else
                        proj.velocity.X += pushForce;

                    if (proj.position.Y < otherProj.position.Y)
                        proj.velocity.Y -= pushForce;
                    else
                        proj.velocity.Y += pushForce;
                    return true;
                }
            }
            return false;
        }
        public static void MinionAntiClump(this Projectile proj, Vector2 pushDir, float pushForce = 0.05f, float randomAngleRange = .05f)
        {
            for (int k = 0; k < Main.maxProjectiles; k++)
            {
                Projectile otherProj = Main.projectile[k];
                if (!otherProj.active || otherProj.owner != proj.owner || !otherProj.minion || k == proj.whoAmI)
                    continue;

                bool sameProjType = otherProj.type == proj.type;
                float taxicabDist = Math.Abs(proj.position.X - otherProj.position.X) + Math.Abs(proj.position.Y - otherProj.position.Y);
                if (sameProjType && taxicabDist < proj.width)
                {
                    proj.velocity += pushDir * pushForce;
                }
            }
        }
        /// <summary>
        /// 复制原版的治疗封装
        /// 重载了一个颜色输出方案，用于特殊用途
        /// </summary>
        /// <param name="Owner"></param>
        /// <param name="healAmt"></param>
        /// <param name="color"></param>
        /// <param name="onlyEffet"></param>
        /// <param name="broadcast"></param>
        public static void ScarletHeal(this Player Owner, int healAmt, Color? color = null, bool onlyEffet = false, bool broadcast = true)
        {
            if (!onlyEffet)
            {
                Owner.statLife += healAmt;
                if (Owner.statLife > Owner.statLifeMax2)
                    Owner.statLife = Owner.statLifeMax2;
            }

            Color c = color ?? CombatText.HealLife;
            CombatText.NewText(new Rectangle((int)Owner.position.X, (int)Owner.position.Y, Owner.width, Owner.height), c, healAmt);
            if (broadcast && Main.netMode == NetmodeID.MultiplayerClient && Owner.whoAmI == Main.myPlayer)
                NetMessage.SendData(MessageID.PlayerHeal, -1, -1, null, Owner.whoAmI, healAmt);
        }
        /// <summary>
        /// 获取一个单位，这里优先判定输入的NPC索引
        /// 如果需要直接忽略npc索引。输入-1
        /// </summary>
        /// <param name="proj"></param>
        /// <param name="target"></param>
        /// <param name="targetIndex"></param>
        /// <param name="searchSecondTarget"></param>
        /// <param name="searchDistance"></param>
        /// <returns></returns>
        public static bool GetTargetSafe(this Projectile proj, out NPC target, int targetIndex, bool searchSecondTarget = true, float searchDistance = 600f, bool canPassWall = false)
        {
            target = null;
            if (targetIndex != -1)
            {
                target = Main.npc[targetIndex];
                if (target.CanBeChasedBy(proj) && target != null)
                    return true;
                else if (searchSecondTarget)
                {
                    target = proj.Center.FindClosestTarget(searchDistance, ignoreTiles: canPassWall);
                    if (target != null && target.CanBeChasedBy(proj))
                        return true;
                }
            }
            else if (searchSecondTarget)
            {
                target = proj.Center.FindClosestTarget(searchDistance, ignoreTiles: canPassWall);
                if (target != null && target.CanBeChasedBy(proj))
                    return true;
            }
            return target != null;
        }
        /// <summary>
        /// 获取一个单位，这里优先判定输入的NPC索引
        /// 直接使用模组内的GlobalTargetIndex
        /// </summary>
        /// <param name="proj"></param>
        /// <param name="target"></param>
        /// <param name="searchSecondTarget"></param>
        /// <param name="searchDistance"></param>
        /// <returns></returns>
        public static bool GetTargetSafe(this Projectile proj, out NPC target, bool searchSecondTarget = true, float searchDistance = 600f, bool canPassWall = false, bool hitLine = false)
        {
            target = null;
            if (proj.YardMod().GlobalTargetIndex != -1)
            {
                target = Main.npc[proj.YardMod().GlobalTargetIndex];
                if (target.CanBeChasedBy() && target != null)
                    return true;
                else if (searchSecondTarget)
                {
                    target = proj.Center.FindClosestTarget(searchDistance, ignoreTiles: canPassWall, hitLine: hitLine);
                    if (target != null && target.CanBeChasedBy(proj))
                        return true;
                }
            }
            else if (searchSecondTarget)
            {
                target = proj.Center.FindClosestTarget(searchDistance, ignoreTiles: canPassWall, hitLine: hitLine);
                if (target != null && target.CanBeChasedBy(proj))
                    return true;
            }
            //默认返回否值
            return false;
        }

        /// <summary>
        /// 用于搜索距离射弹最近的npc单位，并返回NPC实例。通常情况下与上方的追踪方法配套
        /// 这个方法会同时实现穿墙、数组、boss优先度的搜索。不过只能用于射弹。但也足够
        /// 这里Boss优先度的实现逻辑是如果我们但凡搜索到一个Boss，就把这个Boss临时存储，在返回实例的时候优先使用
        /// </summary>
        /// <param name="p">射弹</param>
        /// <param name="maxDist">最大搜索距离</param>
        /// <param name="bossFirst">boss优先度，这个还没实现好逻辑，所以填啥都没用（</param>
        /// <param name="ignoreTiles">穿墙搜索, 默认为</param>
        /// <param name="arrayFirst">数组优先, 这个将会使射弹优先针对数组内第一个单位,默认为否</param>
        /// <returns>返回一个NPC实例</returns>
        public static NPC FindClosestTarget(this Vector2 p, float maxDist, bool bossFirst = false, bool ignoreTiles = true, bool arrayFirst = false, bool hitLine = false)
        {
            //bro我真的要遍历整个NPC吗？
            float distStoraged = maxDist;
            NPC tryGetBoss = null;
            NPC acceptableTarget = null;
            bool alreadyGetBoss = false;
            foreach (NPC npc in Main.ActiveNPCs)
            {
                float exDist = npc.width + npc.height;

                //单位不可被追踪 或者 超出索敌距离则continue
                if (Vector2.Distance(p, npc.Center) > distStoraged + exDist)
                    continue;

                if (!npc.active || npc.friendly || npc.lifeMax < 5 || !npc.CanBeChasedBy(p, false))
                    continue;

                //补: 如果优先搜索Boss单位, 且附近至少有一个。我们直接存储这个Boss单位
                //已经获取到的会被标记，使其不会再跑一遍搜索.
                if (npc.boss && bossFirst && !alreadyGetBoss)
                {
                    tryGetBoss = npc;
                    alreadyGetBoss = true;
                }

                //搜索符合条件的敌人, 准备返回这个NPC实例
                float curNpcDist = Vector2.Distance(npc.Center, p);
                bool isCanHit = false;
                if (hitLine)
                {
                    isCanHit = Collision.CanHit(p, 1, 1, npc.Center, npc.width, npc.height);
                }
                else
                {
                    isCanHit = Collision.CanHit(p, 1, 1, npc.Center, npc.width, npc.height);
                }
                bool canHitline = hitLine && (Collision.CanHitLine(npc.Center, 1, 1, p, 1, 1));
                if (curNpcDist < distStoraged && (ignoreTiles || isCanHit))
                {
                    distStoraged = curNpcDist;
                    acceptableTarget = npc;
                    if (tryGetBoss != null & bossFirst)
                        acceptableTarget = tryGetBoss;
                    //如果是数组优先，直接在这返回实例
                    if (arrayFirst)
                        return acceptableTarget;
                }
            }
            //返回这个NPC实例
            return acceptableTarget;
        }
        public static NPC FindClosestTarget(this Projectile p, float maxDist, Vector2 center, bool bossFirst = false, bool ignoreTiles = true, bool arrayFirst = false, bool hitLine = false)
        {
            //bro我真的要遍历整个NPC吗？
            float distStoraged = maxDist;
            NPC tryGetBoss = null;
            NPC acceptableTarget = null;
            bool alreadyGetBoss = false;
            foreach (NPC npc in Main.ActiveNPCs)
            {
                float exDist = npc.width + npc.height;

                //单位不可被追踪 或者 超出索敌距离则continue
                if (Vector2.Distance(center, npc.Center) > distStoraged + exDist)
                    continue;

                if (!npc.active || npc.friendly || npc.lifeMax < 5 || !npc.CanBeChasedBy(p.Center, false))
                    continue;

                //补: 如果优先搜索Boss单位, 且附近至少有一个。我们直接存储这个Boss单位
                //已经获取到的会被标记，使其不会再跑一遍搜索.
                if (npc.boss && bossFirst && !alreadyGetBoss)
                {
                    tryGetBoss = npc;
                    alreadyGetBoss = true;
                }

                //搜索符合条件的敌人, 准备返回这个NPC实例
                float curNpcDist = Vector2.Distance(npc.Center, center);
                bool canHitline = hitLine && Collision.CanHitLine(npc.Center, 1, 1, center, 1, 1);
                if (curNpcDist < distStoraged && (ignoreTiles || canHitline || Collision.CanHit(center, 1, 1, npc.Center, 1, 1)))
                {
                    distStoraged = curNpcDist;
                    acceptableTarget = npc;
                    if (tryGetBoss != null & bossFirst)
                        acceptableTarget = tryGetBoss;
                    //如果是数组优先，直接在这返回实例
                    if (arrayFirst)
                        return acceptableTarget;
                }
            }
            //返回这个NPC实例
            return acceptableTarget;
        }

        /// <summary>
        /// 用于跟踪指定地点的方法
        /// 只会跟踪你传进去的目标
        /// </summary>
        /// <param name="proj">射弹</param>
        /// <param name="target">射弹目标</param>
        /// <param name="distRequired">最大范围</param>
        /// <param name="speed">射弹速度</param>
        /// <param name="inertia">惯性</param>
        /// <param name="maxAngleChage">角度限制，默认为空. </param>
        public static void HomingTarget(this Projectile proj, Vector2 target, float distRequired, float speed, float inertia, float? maxAngleChage = null)
        {
            if (distRequired > 0 && Vector2.Distance(proj.Center, target) > distRequired)
                return;
            //开始追踪target
            Vector2 home = (target - proj.Center).SafeNormalize(Vector2.UnitY);
            Vector2 velo = (proj.velocity * inertia + home * speed) / (inertia + 1f);
            //这里给了一个角度限制
            if (maxAngleChage.HasValue)
            {
                float curAngle = proj.velocity.ToRotation();
                float tarAngle = velo.ToRotation();
                float angleDiffer = WrapAngle(tarAngle - curAngle);
                //转弧度
                float maxRadians = ToRadians(maxAngleChage.Value);
                if (Math.Abs(angleDiffer) > maxRadians)
                {
                    float clampedAngle = curAngle + Math.Sign(angleDiffer) * maxRadians;
                    float setSpeed = velo.Length();
                    velo = new Vector2((float)Math.Cos(clampedAngle), (float)Math.Sin(clampedAngle)) * setSpeed;
                }
            }
            //设定速度
            proj.velocity = velo;
        }
        public static bool IsLegal(this NPC target) => target != null && target.CanBeChasedBy();
        public static bool IsOutScreen(this Projectile proj, float mult = 1f) => OutOffScreen(proj.Center, mult);
        public static void EnterHudArea(BlendState blendState)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, blendState, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullNone, null, Main.UIScaleMatrix);
        }
        public static void EnterHudArea(BlendState blendState, SamplerState samplerState)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, blendState, samplerState, DepthStencilState.None, RasterizerState.CullNone, null, Main.UIScaleMatrix);
        }

        public static void EndHudArea()
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullNone, null, Main.UIScaleMatrix);
        }
        public static void AddCommon(this ItemLoot item, int itemID, int dropRateInt = 1, int minQuantity = 1, int maxQuantity = 1)
        {
            item.Add(ItemDropRule.Common(itemID, dropRateInt, minQuantity, maxQuantity));
        }
    }
}
