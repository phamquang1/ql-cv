using System;
using System.Collections.Generic;

namespace NCCTalentManagement.Extensions
{
    public interface IOption
    {
        bool IsSome { get; }
        bool IsNone { get; }
        object Value { get; }
    }

    public class Option<TValue> : IOption
    {
        public Option(TValue value)
        {
            _Value = value;
            _IsSome = true;
        }

        private Option()
        {
            _IsSome = false;
        }

        private bool _IsSome;

        public bool IsSome
        {
            get { return _IsSome; }
        }
        private TValue _Value;

        public bool IsNone
        {
            get { return !_IsSome; }
        }

        public TValue Value
        {
            get
            {
                if (IsNone)
                    throw new Exception("Attempt is made to retrieve value from the None option.");
                return _Value;
            }
        }

        public TValue GetValueOrDefault()
        {
            return GetValue(() => default);
        }

        public TValue GetValue(Func<TValue> defautlValueGetter)
        {
            if (IsSome)
                return Value;
            else
                return defautlValueGetter();
        }

        public Option<TValue2> Select<TValue2>(Func<TValue, Option<TValue2>> selector)
        {
            if (IsSome)
                return selector(Value);
            else
                return Option<TValue2>.None;
        }

        public void Iter(Action<TValue> action)
        {
            if (IsSome)
                action(Value);
        }

        internal static Option<TValue> _None = new Option<TValue>();

        public static Option<TValue> None
        {
            get
            {
                return _None;
            }
        }
        public IEnumerable<TValue> AsEnumerable()
        {
            if (IsSome)
                yield return Value;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Option<TValue>;
            return other != null && (IsNone && other.IsNone || IsSome && other.IsSome && Equals(Value, other.Value));
        }

        public override int GetHashCode()
        {
            if (IsNone)
                return -1;

            var o = (object)Value;
            if (o == null)
                return 0;

            return o.GetHashCode();
        }

        bool IOption.IsSome
        {
            get { return IsSome; }
        }

        object IOption.Value
        {
            get { return Value; }
        }
    }

    public static class Option
    {
        public static Option<TValue> Some<TValue>(TValue value)
        {
            return new Option<TValue>(value);
        }
        public static Option<T> OfReference<T>(T referenceValue)
        {
            return referenceValue == null ? Option<T>.None : Some(referenceValue);
        }
        public static bool IsSome<T>(Option<T> option)
        {
            return option.IsSome;
        }
        public static T ValueOf<T>(Option<T> option)
        {
            return option.Value;
        }
    }
}
