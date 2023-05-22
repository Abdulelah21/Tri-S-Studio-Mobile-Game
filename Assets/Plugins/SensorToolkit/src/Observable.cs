﻿using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Micosmo.SensorToolkit {

    [Serializable]
    public class ObservableGameObject : Observable<GameObject> { }

    [Serializable]
    public class ObservableTransform : Observable<Transform> { }

    [Serializable]
    public class ObservableVector3 : Observable<Vector3> { }

    [Serializable]
    public class ObservableBool : Observable<bool> { }

    [Serializable]
    public class ObservableInt : Observable<int> { }

    [Serializable]
    public class ObservableFloat : Observable<float> { }

    [Serializable]
    public class Observable<T> : ObservableBase, IEquatable<Observable<T>> {

        [SerializeField]
        T value;

        T prevValue;
        bool prevValueInitialized = false;

        public override event Action OnChanged;
        public event Action<T, T> OnChangedValues;

        protected override string ValuePropName { get { return "value"; } }

        public Observable() {
        }

        public Observable(T value) {
            this.value = value;
        }

        public T Value {
            get { return value; }
            set {
                if (EqualityComparer<T>.Default.Equals(value, this.value)) {
                    return;
                }

                prevValue = value;
                prevValueInitialized = true;
                var storePrev = this.value;
                this.value = value;
                if (OnChanged != null) {
                    OnChanged();
                }
                if (OnChangedValues != null) {
                    OnChangedValues(storePrev, value);
                }
            }
        }

        public static explicit operator T(Observable<T> observable) {
            return observable.value;
        }

        public override string ToString() {
            return value.ToString();
        }

        public bool Equals(Observable<T> other) {
            if (other == null) {
                return false;
            }
            return other.value.Equals(value);
        }

        public override bool Equals(object other) {
            return other != null
                && other is Observable<T>
                && ((Observable<T>)other).value.Equals(value);
        }

        public override int GetHashCode() {
            return value.GetHashCode();
        }

        protected override void OnBeginGui() {
            prevValue = value;
            prevValueInitialized = true;
        }

        public override void OnValidate() {
            if (prevValueInitialized) {
                var nextValue = value;
                value = prevValue;
                Value = nextValue;
            } else {
                prevValue = value;
                prevValueInitialized = true;
            }
        }
    }
}