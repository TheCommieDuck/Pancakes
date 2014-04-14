using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WaffleCat.Core;
using WaffleCat.Core.Components;
using WaffleCat.Core.Entities;
using WaffleCat.Core.Systems;

namespace WaffleCat
{
    class FloorSystem : Subsystem
    {
        public FloorSystem()
            :base(typeof(FloorComponent), typeof(VertexBufferComponent)) {}

        public override void Process(Entity entity, GameTime gameTime)
        {
            //do nothing
        }

        public override void InitializeEntity(Entity entity)
        {
            FloorComponent floor = entity.Get<FloorComponent>();
            //build the floor
            int count = 0;
            List<VertexPositionColor> vertices = new List<VertexPositionColor>();
            Random r = new Random();
            for (int x = 0; x < floor.Width; ++x)
            {
                count++;
                for (int z = 0; z < floor.Height; ++z)
                {
                    foreach (VertexPositionColor vertex in FloorTiles(x, z, FloorComponent.FloorColors[r.Next(FloorComponent.FloorColors.Count())]))
                        vertices.Add(vertex);
                    count++;
                }
            }

            entity.Get<VertexBufferComponent>().Buffer = new VertexBuffer(Blackboard.Get<GraphicsDevice>("GraphicsDevice"),
                VertexPositionColor.VertexDeclaration, vertices.Count, BufferUsage.WriteOnly);
            entity.Get<VertexBufferComponent>().Buffer.SetData<VertexPositionColor>(vertices.ToArray());
        }

        private IEnumerable<VertexPositionColor> FloorTiles(int x, int z, Color color)
        {
            yield return new VertexPositionColor(new Vector3(x, 0, z), color);
            yield return new VertexPositionColor(new Vector3(1 + x, 0, z), color);
            yield return new VertexPositionColor(new Vector3(x, 0, 1 + z), color);
            yield return new VertexPositionColor(new Vector3(1 + x, 0, z), color);
            yield return new VertexPositionColor(new Vector3(1 + x, 0, 1 + z), color);
            yield return new VertexPositionColor(new Vector3(x, 0, 1 + z), color);
        }
    }
}
