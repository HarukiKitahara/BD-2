using System;
namespace MyProject.Core
{
    public class CommonValue
    {
        public float Value { get; private set; }
        public float Min { get; private set; }
        public float Max { get; private set; }
        public float Ratio => (Value - Min) / (Max - Min);
        public event Action ValueChangeEvent, ValueMinEvent, ValueMaxEvent;
        public CommonValue(float initValue, float min, float max)
        {
            if (min >= max || initValue > max || initValue < min) throw new Exception("AttributeValue³õÊ¼»¯³¬³ö·¶Î§");
            Value = initValue;
            Min = min;
            Max = max;
        }
        public void TryChangeValue(float amount)
        {
            if (amount == 0) return;
            var temp = Value + amount;
            if (temp >= Max)
            {
                Value = Max;
                ValueMaxEvent?.Invoke();
            }
            else if (temp <= Min)
            {
                Value = Min;
                ValueMinEvent?.Invoke();
            }
            else
            {
                Value = temp;
            }
            ValueChangeEvent?.Invoke();
        }
    }
}
