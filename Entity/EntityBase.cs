using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Entity
{
    public abstract class EntityBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public TResult Clone<TResult>()
        {
            return JsonConvert.DeserializeObject<TResult>(this.ToJson());
        }

        public static TResult FromJson<TResult>(string json)
        {
            return JsonConvert.DeserializeObject<TResult>(json);
        }
    }

}
