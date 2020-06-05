namespace O9K.Evader.Helpers
{
    using Core.Extensions;

    using Ensage;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Geometry;

    using SharpDX;

    internal class AbilityObstacleDrawer
    {
        public enum Type
        {
            Any,

            Circle,

            Rectangle
        }

        private static readonly Vector3 ParticleColor = new Vector3(255, 100, 50);

        private readonly ParticleEffect[] rectangle = new ParticleEffect[4];

        private ParticleEffect circle;

        public void Dispose(Type figure = Type.Any)
        {
            switch (figure)
            {
                case Type.Any:
                    this.DisposeCircle();
                    this.DisposeRectangle();
                    break;
                case Type.Circle:
                    this.DisposeCircle();
                    break;
                case Type.Rectangle:
                    this.DisposeRectangle();
                    break;
            }
        }

        public void DrawArcRectangle(Vector3 startPosition, Vector3 endPosition, float startRadius, float endRadius = 0)
        {
            if (this.rectangle[0] != null)
            {
                return;
            }

            if (endRadius <= 0)
            {
                endRadius = startRadius;
            }

            var difference = startPosition - endPosition;
            var rotation = difference.Rotated(MathUtil.DegreesToRadians(90));
            rotation.Normalize();

            var start = rotation * startRadius;
            var end = rotation * endRadius;

            var correctedEnd = startPosition.Extend2D(endPosition, startPosition.Distance2D(endPosition) - (endRadius * 0.45f));

            var rightStartPosition = startPosition + start;
            var leftStartPosition = startPosition - start;
            var rightEndPosition = correctedEnd + end;
            var leftEndPosition = correctedEnd - end;

            this.rectangle[0] = this.DrawLine(rightStartPosition, rightEndPosition);
            this.rectangle[1] = this.DrawLine(rightStartPosition, leftStartPosition);
            this.rectangle[2] = this.DrawLine(leftStartPosition, leftEndPosition);
            this.rectangle[3] = this.DrawArc(endPosition, startPosition, endRadius);
        }

        public void DrawCircle(Vector3 position, float radius)
        {
            if (this.circle != null)
            {
                return;
            }

            this.circle = new ParticleEffect(@"materials\ensage_ui\particles\drag_selected_ring_mod.vpcf", position);
            this.circle.SetControlPoint(1, ParticleColor);
            this.circle.SetControlPoint(2, new Vector3(radius * -1, 255, 0));
        }

        public void DrawDoubleArcRectangle(Vector3 startPosition, Vector3 endPosition, float startRadius, float endRadius = 0)
        {
            if (this.rectangle[0] != null)
            {
                return;
            }

            if (endRadius <= 0)
            {
                endRadius = startRadius;
            }

            var difference = startPosition - endPosition;
            var rotation = difference.Rotated(MathUtil.DegreesToRadians(90));
            rotation.Normalize();

            var start = rotation * startRadius;
            var end = rotation * endRadius;

            var correctedEnd = startPosition.Extend2D(endPosition, startPosition.Distance2D(endPosition) - (endRadius * 0.45f));

            var correctedStart = startPosition.Extend2D(endPosition, startRadius);

            var rightStartPosition = correctedStart + start;
            var leftStartPosition = correctedStart - start;
            var rightEndPosition = correctedEnd + end;
            var leftEndPosition = correctedEnd - end;

            this.rectangle[0] = this.DrawLine(rightStartPosition, rightEndPosition);
            this.rectangle[1] = this.DrawArc(startPosition.Extend2D(endPosition, startRadius * 0.55f), endPosition, startRadius);
            this.rectangle[2] = this.DrawLine(leftStartPosition, leftEndPosition);
            this.rectangle[3] = this.DrawArc(endPosition, startPosition, endRadius);
        }

        public void DrawRectangle(Vector3 startPosition, Vector3 endPosition, float startWidth, float endWidth = 0)
        {
            if (this.rectangle[0] != null)
            {
                return;
            }

            if (endWidth <= 0)
            {
                endWidth = startWidth;
            }

            var difference = startPosition - endPosition;
            var rotation = difference.Rotated(MathUtil.DegreesToRadians(90));
            rotation.Normalize();

            var start = rotation * startWidth;
            var end = rotation * endWidth;

            var rightStartPosition = startPosition + start;
            var leftStartPosition = startPosition - start;
            var rightEndPosition = endPosition + end;
            var leftEndPosition = endPosition - end;

            this.rectangle[0] = this.DrawLine(rightStartPosition, rightEndPosition);
            this.rectangle[1] = this.DrawLine(rightStartPosition, leftStartPosition);
            this.rectangle[2] = this.DrawLine(leftStartPosition, leftEndPosition);
            this.rectangle[3] = this.DrawLine(leftEndPosition, rightEndPosition);
        }

        public void UpdateCirclePosition(Vector3 position)
        {
            this.circle?.SetControlPoint(0, position);
        }

        public void UpdateRectanglePosition(Vector3 startPosition, Vector3 endPosition, float startWidth, float endWidth = 0)
        {
            if (this.rectangle[0] == null)
            {
                return;
            }

            if (endWidth <= 0)
            {
                endWidth = startWidth;
            }

            endPosition = startPosition.Extend2D(endPosition, startPosition.Distance2D(endPosition) + (endWidth / 2));

            var difference = startPosition - endPosition;
            var rotation = difference.Rotated(MathUtil.DegreesToRadians(90));
            rotation.Normalize();

            var start = rotation * startWidth;
            var end = rotation * endWidth;

            var rightStartPosition = startPosition + start;
            var leftStartPosition = startPosition - start;
            var rightEndPosition = endPosition + end;
            var leftEndPosition = endPosition - end;

            this.rectangle[0].SetControlPoint(1, rightStartPosition);
            this.rectangle[0].SetControlPoint(2, rightEndPosition);

            this.rectangle[1].SetControlPoint(1, rightStartPosition);
            this.rectangle[1].SetControlPoint(2, leftStartPosition);

            this.rectangle[2].SetControlPoint(1, leftStartPosition);
            this.rectangle[2].SetControlPoint(2, leftEndPosition);

            this.rectangle[3].SetControlPoint(1, leftEndPosition);
            this.rectangle[3].SetControlPoint(2, rightEndPosition);
        }

        private void DisposeCircle()
        {
            this.circle?.Dispose();
            this.circle = null;
        }

        private void DisposeRectangle()
        {
            for (var i = 0; i < this.rectangle.Length; i++)
            {
                this.rectangle[i]?.Dispose();
                this.rectangle[i] = null;
            }
        }

        private ParticleEffect DrawArc(Vector3 startPosition, Vector3 endPosition, float radius)
        {
            var arc = new ParticleEffect(@"materials\ensage_ui\particles\semicircle_v2.vpcf", startPosition);
            arc.SetControlPoint(1, endPosition);
            arc.SetControlPoint(2, new Vector3(radius * 1.12f, 0, 0));
            arc.SetControlPoint(3, ParticleColor);
            arc.SetControlPoint(4, new Vector3(255, 15, 0));

            return arc;
        }

        private ParticleEffect DrawLine(Vector3 startPosition, Vector3 endPosition)
        {
            var line = new ParticleEffect(@"materials\ensage_ui\particles\line.vpcf", startPosition);
            line.SetControlPoint(2, endPosition);
            line.SetControlPoint(3, new Vector3(255, 15, 0));
            line.SetControlPoint(4, ParticleColor);

            return line;
        }
    }
}