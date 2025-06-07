using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoGame.Jolpango.UI.Elements.Containers
{
    public enum Orientation
    {
        Horizontal,
        Vertical
    }
    public enum ItemAlignment
    {
        Left,
        Right,
        Center
    }
    public class UIStackPanel : UIContainer
    {
        public Orientation Orientation { get; set; }
        public float Gap { get; set; }
        public ItemAlignment AlignItems { get; set; } = ItemAlignment.Left;
        public override void RecalculateLayout()
        {
            Vector2 offset = Padding;
            float maxCrossSize = 0;

            foreach (var child in Children)
            {
                if (child is UIContainer container)
                    container.RecalculateLayout();

                child.Position = offset;

                Vector2 childSize = child.EffectiveSize;

                if (Orientation == Orientation.Horizontal)
                {
                    offset.X += childSize.X + Gap;
                    maxCrossSize = Math.Max(maxCrossSize, childSize.Y);
                }
                else
                {
                    offset.Y += childSize.Y + Gap;
                    maxCrossSize = Math.Max(maxCrossSize, childSize.X);
                }
            }

            // Final size of panel (without trailing gap)
            if (Orientation == Orientation.Horizontal)
                Size = new Vector2(offset.X - Gap + Padding.X, maxCrossSize + Padding.Y * 2);
            else
                Size = new Vector2(maxCrossSize + Padding.X * 2, offset.Y - Gap + Padding.Y);

            // Ensure Size respects MinSize
            Size = new Vector2(
                Math.Max(Size.X, MinSize.X),
                Math.Max(Size.Y, MinSize.Y)
            );

            // Align children
            foreach (var child in Children)
            {
                Vector2 childSize = child.EffectiveSize;
                Vector2 pos = child.Position;

                if (Orientation == Orientation.Horizontal)
                {
                    float crossSize = Size.Y - Padding.Y * 2; // Use full panel cross size
                    switch (AlignItems)
                    {
                        case ItemAlignment.Center:
                            pos.Y = Padding.Y + (crossSize - childSize.Y) / 2f;
                            break;
                        case ItemAlignment.Right:
                            pos.Y = Padding.Y + (crossSize - childSize.Y);
                            break;
                        case ItemAlignment.Left:
                        default:
                            pos.Y = Padding.Y;
                            break;
                    }
                }
                else
                {
                    float crossSize = Size.X - Padding.X * 2; // Use full panel cross size
                    switch (AlignItems)
                    {
                        case ItemAlignment.Center:
                            pos.X = Padding.X + (crossSize - childSize.X) / 2f;
                            break;
                        case ItemAlignment.Right:
                            pos.X = Padding.X + (crossSize - childSize.X);
                            break;
                        case ItemAlignment.Left:
                        default:
                            pos.X = Padding.X;
                            break;
                    }
                }

                child.Position = new Vector2(
                    Orientation == Orientation.Horizontal ? child.Position.X : pos.X,
                    Orientation == Orientation.Vertical ? child.Position.Y : pos.Y
                );
            }
        }
    }
}
