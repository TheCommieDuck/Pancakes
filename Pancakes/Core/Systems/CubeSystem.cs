using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WaffleCat.Core.Components;
using WaffleCat.Core.Entities;

namespace WaffleCat.Core.Systems
{
    class CubeSystem : Subsystem
    {
        public CubeSystem()
            : base(typeof(CubeComponent), typeof(VertexBufferComponent), typeof(Transform3DComponent))
        {
        }

        public override void InitializeEntity(Entity entity)
        {
            List<VertexPositionColor> vertices = new List<VertexPositionColor>();

            vertices.AddRange(BuildFace(new Vector3(0, 0, 0), new Vector3(0, 1, 1), Color.Blue));
            vertices.AddRange(BuildFace(new Vector3(0, 0, 1), new Vector3(1, 1, 1), Color.DarkOrange));
            vertices.AddRange(BuildFace(new Vector3(1, 0, 1), new Vector3(1, 1, 0), Color.DarkSalmon));
            vertices.AddRange(BuildFace(new Vector3(1, 0, 0), new Vector3(0, 1, 0), Color.Gold));

            vertices.AddRange(BuildHorizontalFace(new Vector3(0, 1, 0), new Vector3(1, 1, 1), Color.Gray));
            vertices.AddRange(BuildHorizontalFace(new Vector3(0, 0, 1), new Vector3(1, 0, 0), Color.DeepPink));

            entity.Get<VertexBufferComponent>().Buffer = new VertexBuffer(Blackboard.Get<GraphicsDevice>("GraphicsDevice"), 
                VertexPositionColor.VertexDeclaration, vertices.Count, BufferUsage.WriteOnly);
            entity.Get<VertexBufferComponent>().Buffer.SetData<VertexPositionColor>(vertices.ToArray());
        }

        private List<VertexPositionColor> BuildFace(Vector3 p1, Vector3 p2, Color color)
        {
            List<VertexPositionColor> verts = new List<VertexPositionColor>();
            verts.Add(BuildVertex(p1.X, p1.Y, p1.Z, color));
            verts.Add(BuildVertex(p1.X, p2.Y, p1.Z, color));
            verts.Add(BuildVertex(p2.X, p2.Y, p2.Z, color));

            verts.Add(BuildVertex(p2.X, p2.Y, p2.Z, color));
            verts.Add(BuildVertex(p2.X, p1.Y, p2.Z, color));
            verts.Add(BuildVertex(p1.X, p1.Y, p1.Z, color));

            return verts;
        }

        private List<VertexPositionColor> BuildHorizontalFace(Vector3 p1, Vector3 p2, Color color)
        {
            List<VertexPositionColor> vertices = new List<VertexPositionColor>();

            vertices.Add(BuildVertex(p1.X, p1.Y, p1.Z, color));
            vertices.Add(BuildVertex(p2.X, p1.Y, p1.Z, color));
            vertices.Add(BuildVertex(p2.X, p2.Y, p2.Z, color));
            vertices.Add(BuildVertex(p1.X, p1.Y, p1.Z, color));
            vertices.Add(BuildVertex(p2.X, p2.Y, p2.Z, color));
            vertices.Add(BuildVertex(p1.X, p1.Y, p2.Z, color));

            return vertices;
        }

        private VertexPositionColor BuildVertex(float x, float y, float z, Color color)
        {
            return new VertexPositionColor(new Vector3(x, y, z), color);
        }
    }
}
