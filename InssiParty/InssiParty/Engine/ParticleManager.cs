using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InssiParty.Engine
{
    class Particle
    {
        public Texture2D texture { get; set; }
        public int ttl { get; set; } //time to live
        public Vector2 pos { get; set; }
        public Vector2 vel { get; set; }
    }

    /**
     * Provides particle effects.
     */
    class ParticleManager
    {
        private List<Particle> particleList;
        private Random random;

        public ParticleManager()
        {
            random = new Random();
            particleList = new List<Particle>(5000);

            Console.WriteLine("[ParticleManager] Init ok!");
        }

        public void UpdateParticles()
        {
            int i;
            //Handle updating
            for (i = 0; i < particleList.Count; ++i)
            {
                particleList[i].pos += particleList[i].vel;
                particleList[i].ttl--;

                if (particleList[i].ttl < 0)
                {
                    particleList.Remove(particleList[i]);
                    --i;
                }
            }
            Console.WriteLine("ParticleCount: " + i);
        }

        public void AddParticle(Texture2D texture, Vector2 position, Vector2 minSpeed, Vector2 maxSpeed, int ttlMin, int ttlMax,int count)
        {
            for (int i = 0; i < count; ++i)
            {
                Particle particle = new Particle();

                particle.texture = texture;
                particle.pos = position;
                particle.ttl = random.Next(ttlMin, ttlMax);

                particle.vel = new Vector2(random.Next(0, 20) - 10, random.Next(0, 20) - 10);

                particleList.Add(particle);
            }
        }

        public void RenderParticles(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            for (int i = 0; i < particleList.Count; ++i)
            {
                spriteBatch.Draw(particleList[i].texture , particleList[i].pos , Color.White);
            }
        }

    }
}
