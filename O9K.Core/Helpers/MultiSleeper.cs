namespace O9K.Core.Helpers
{
    using System.Collections;
    using System.Collections.Generic;

    public sealed class MultiSleeper : MultiSleeper<uint>
    {
    }

    public class MultiSleeper<T> : IEnumerable<KeyValuePair<T, Sleeper>>
    {
        private readonly Dictionary<T, Sleeper> sleepers = new Dictionary<T, Sleeper>();

        public Sleeper this[T key]
        {
            get
            {
                if (!this.sleepers.TryGetValue(key, out var sleeper))
                {
                    this.sleepers[key] = sleeper = new Sleeper();
                }

                return sleeper;
            }
            set
            {
                this.sleepers[key] = value;
            }
        }

        public void ExtendSleep(T key, float seconds)
        {
            this[key].ExtendSleep(seconds);
        }

        public IEnumerator<KeyValuePair<T, Sleeper>> GetEnumerator()
        {
            return this.sleepers.GetEnumerator();
        }

        public bool IsSleeping(T key)
        {
            return this[key].IsSleeping;
        }

        public void Remove(T key)
        {
            this.sleepers.Remove(key);
        }

        public void Reset(T key)
        {
            this[key].Reset();
        }

        public void Reset()
        {
            this.sleepers.Clear();
        }

        public void Sleep(T key, float seconds)
        {
            this[key].Sleep(seconds);
        }

        public void SleepUntil(T key, float seconds)
        {
            this[key].SleepUntil(seconds);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}