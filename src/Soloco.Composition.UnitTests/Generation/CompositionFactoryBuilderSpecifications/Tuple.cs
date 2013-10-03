using System;

namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    public class Tuple<T> : IEquatable<Tuple<T>>
    {
        public T Part1 { get; set; }
        public T Part2 { get; set; }

        public Tuple(T part1, T part2)
        {
            Part1 = part1;
            Part2 = part2;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.
        ///                 </param>
        public bool Equals(Tuple<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Part1, Part1) && Equals(other.Part2, Part2);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. 
        ///                 </param><exception cref="T:System.NullReferenceException">The <paramref name="obj"/> parameter is null.
        ///                 </exception><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Tuple<T>)) return false;
            return Equals((Tuple<T>) obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            unchecked
            {
                return (Part1.GetHashCode()*397) ^ Part2.GetHashCode();
            }
        }

        public static bool operator ==(Tuple<T> left, Tuple<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Tuple<T> left, Tuple<T> right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return string.Format("Part1: {0}, Part2: {1}", Part1, Part2);
        }
    }
}