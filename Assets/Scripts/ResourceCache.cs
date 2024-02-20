﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class ResourceCache<T> where T : Object
{
        
     
    private readonly Dictionary<String, T> _objects = new();

    /**
         * Si le path est contenu dans le dictionnaire, alors on retourne la valeur associée.
         * Si non, bah on la load, et on l'ajoute au cache.
         *
         * <param name="path">Chemin d'accès demandé</param>
         */
    public T Get(string path)
    {
        if(!this._objects.ContainsKey(path))
            this._objects.Add(path, Resources.Load<T>(path));
        return this._objects.GetValueOrDefault(path);
    }

}