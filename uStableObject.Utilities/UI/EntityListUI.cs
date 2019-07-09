﻿using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using uStableObject.Data;
using uStableObject.Utilities;

namespace                                   uStableObject.UI
{
    public abstract class                   EntityListUI<T, R, E> : MonoBehaviour
                                            where T : IEntity
                                            where E : UnityEvent<T>
                                            where R : EntityRow<T, E>
    {
        #region Input Data
        [SerializeField] EntityListVar      _list;
        [SerializeField] RectTransform      _instancesRoot;
        [SerializeField] R                  _entityRowPrefab;
        [SerializeField] int                _entitiesPerRow = 1;
        [SerializeField] bool               _autoSelectIfNoPreselected;
        #endregion

        #region Members
        int                                 _prevEntityCount;
        List<R>                             _instances = new List<R>();
        #endregion

        #region Properties
        public T                            SelectedEntity { get; set; }
        public IEntityListVar               ListVar { get; set; }
        #endregion

        #region Unity
        void                                Awake()
        {
            this.ListVar = this._list;
        }
        #endregion

        #region Triggers
        public void                         Refresh()
        {
            int                             i = 0;

            Debug.Log("List " + this._list.name + " UI Refresh");
            if (this._autoSelectIfNoPreselected
                && this.SelectedEntity == null)
            {
                foreach (var entity in this.ListVar.Entities)
                {
                    this.SelectedEntity = (T)entity;
                    break;
                }
            }

            foreach (var entity in this.ListVar.Entities)
            {
                UnityEngine.Profiling.Profiler.BeginSample("RefreshEntities");
                var entityRow = this.GetEntityRow(i);
                entityRow.SetEntity((T)entity);
                if (Object.Equals(entity, this.SelectedEntity))
                {
                    entityRow.PreSelected();
                }
                UnityEngine.Profiling.Profiler.EndSample();
                ++i;
            }
            for (var n = this._instances.Count - 1; n >= i; --n)
            {
                var instance = this._instances[n];
                if (instance.gameObject.activeSelf)
                {
                    instance.gameObject.SetActive(false);
                }
                instance.transform.SetParent(this.transform);
                this._instances.RemoveAt(n);
                GoPool.Despawn(instance);
            }
            if (this._prevEntityCount != this._instances.Count)
            {
                this._prevEntityCount = this._instances.Count;
                this.ResizeScrollView();
            }
        }
        #endregion

        #region Helpers
        R                                   GetEntityRow(int num)
        {
            R                               rowInstance;

            if (num >= this._instances.Count)
            {
                float rowWidth = (this._entityRowPrefab.transform as RectTransform).rect.width;
                float rowheight = (this._entityRowPrefab.transform as RectTransform).rect.height;
                int col = num % this._entitiesPerRow;
                int row = num / this._entitiesPerRow;
                Vector3 localPos = new Vector3(col * rowWidth, (-row) * rowheight, 0);
                rowInstance = GoPool.Spawn(this._entityRowPrefab, this._instancesRoot.TransformPoint(localPos), Quaternion.identity, this._instancesRoot);
                rowInstance.transform.SetSiblingIndex(num);
                this._instances.Add(rowInstance);
            }
            else
            {
                rowInstance = this._instances[num];
                if (!rowInstance.gameObject.activeSelf)
                {
                    rowInstance.gameObject.SetActive(true);
                }
            }
            return (rowInstance);
        }

        void                                ResizeScrollView()
        {
            float rowheight = (this._entityRowPrefab.transform as RectTransform).rect.height;
            this._instancesRoot.sizeDelta = new Vector2(this._instancesRoot.sizeDelta.x, (this._instances.Count / this._entitiesPerRow) * rowheight);
        }
        #endregion
    }
}
