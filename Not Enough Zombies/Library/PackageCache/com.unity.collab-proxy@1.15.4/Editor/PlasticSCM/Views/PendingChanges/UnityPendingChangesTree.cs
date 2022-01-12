﻿using System.Collections.Generic;

using Codice.Client.BaseCommands;
using Codice.Client.Commands;
using PlasticGui;
using PlasticGui.WorkspaceWindow.PendingChanges;

namespace Unity.PlasticSCM.Editor.Views.PendingChanges
{
    internal class UnityPendingChangesTree
    {
        internal UnityPendingChangesTree()
        {
            mInnerTree = new PendingChangesTree();
            mMetaCache = new MetaCache();
        }

        internal List<PendingChangeCategory> GetNodes()
        {
            return mInnerTree.GetNodes();
        }

        internal bool HasMeta(ChangeInfo changeInfo)
        {
            return mMetaCache.ContainsMeta(changeInfo);
        }

        internal ChangeInfo GetMetaChange(ChangeInfo change)
        {
            return mMetaCache.GetExistingMeta(change);
        }

        internal void FillWithMeta(List<ChangeInfo> changes)
        {
            changes.AddRange(
                mMetaCache.GetExistingMeta(changes));
        }

        internal void GetCheckedChanges(
            bool bExcludePrivates,
            out List<ChangeInfo> changes,
            out List<ChangeInfo> dependenciesCandidates)
        {
            mInnerTree.GetCheckedChanges(
                bExcludePrivates,
                out changes,
                out dependenciesCandidates);

            changes.AddRange(
                mMetaCache.GetExistingMeta(changes));
            dependenciesCandidates.AddRange(
                mMetaCache.GetExistingMeta(dependenciesCandidates));
        }

        internal List<ChangeInfo> GetDependenciesCandidates(
            List<ChangeInfo> selectedChanges,
            bool bExcludePrivates)
        {
            return mInnerTree.GetDependenciesCandidates(
                selectedChanges, bExcludePrivates);
        }

        internal void BuildChangeCategories(
            string wkPath,
            PlasticGui.WorkspaceWindow.PendingChanges.PendingChanges pendingChanges,
            CheckedStateManager checkedStateManager)
        {
            mMetaCache.Build(pendingChanges);

            mInnerTree.BuildChangeCategories(
                wkPath,
                pendingChanges,
                checkedStateManager);
        }

        internal void Filter(Filter filter, List<string> columnNames)
        {
            mInnerTree.Filter(filter, columnNames);
        }

        internal void Sort(string key, bool ascending)
        {
            mInnerTree.Sort(key, ascending);
        }

        MetaCache mMetaCache;
        PendingChangesTree mInnerTree;

        class MetaCache
        {
            internal bool ContainsMeta(ChangeInfo changeInfo)
            {
                string key = BuildKey.ForMetaChange(changeInfo);

                return mCache.ContainsKey(key);
            }

            internal ChangeInfo GetExistingMeta(ChangeInfo change)
            {
                ChangeInfo result;

                if (!mCache.TryGetValue(BuildKey.ForMetaChange(change), out result))
                    return null;

                return result;
            }

            internal List<ChangeInfo> GetExistingMeta(
                List<ChangeInfo> changes)
            {
                List<ChangeInfo> result = new List<ChangeInfo>();

                foreach (ChangeInfo change in changes)
                {
                    string key = BuildKey.ForMetaChange(change);

                    ChangeInfo metaChange;
                    if (!mCache.TryGetValue(key, out metaChange))
                        continue;

                    result.Add(metaChange);
                }

                return result;
            }

            internal void Build(PlasticGui.WorkspaceWindow.PendingChanges.PendingChanges pendingChanges)
            {
                mCache.Clear();

                ExtractMetaToCache(pendingChanges.Added, mCache);
                ExtractMetaToCache(pendingChanges.Deleted, mCache);
                ExtractMetaToCache(pendingChanges.Changed, mCache);
                ExtractMetaToCache(pendingChanges.Moved, mCache);
            }

            static void ExtractMetaToCache(
                List<ChangeInfo> changes,
                Dictionary<string, ChangeInfo> cache)
            {
                HashSet<string> indexedKeys = BuildIndexedKeys(changes);

                for (int i = changes.Count - 1; i >= 0; i--)
                {
                    ChangeInfo currentChange = changes[i];

                    if (!MetaPath.IsMetaPath(currentChange.Path))
                        continue;

                    string realPath = MetaPath.GetPathFromMetaPath(currentChange.Path);

                    if (!indexedKeys.Contains(BuildKey.BuildCacheKey(
                        currentChange.ChangeTypes, realPath)))
                        continue;

                    // found foo.c and foo.c.meta
                    // with the same chage types - move .meta to cache
                    cache.Add(BuildKey.ForChange(currentChange), currentChange);
                    changes.RemoveAt(i);
                }
            }

            static HashSet<string> BuildIndexedKeys(
                List<ChangeInfo> changes)
            {
                HashSet<string> result = new HashSet<string>();

                foreach (ChangeInfo change in changes)
                {
                    if (MetaPath.IsMetaPath(change.Path))
                        continue;

                    result.Add(BuildKey.ForChange(change));
                }

                return result;
            }

            Dictionary<string, ChangeInfo> mCache =
                new Dictionary<string, ChangeInfo>();

            static class BuildKey
            {
                internal static string ForChange(ChangeInfo change)
                {
                    return BuildCacheKey(
                        change.ChangeTypes,
                        change.Path);
                }

                internal static string ForMetaChange(ChangeInfo change)
                {
                    return BuildCacheKey(
                        change.ChangeTypes,
                        MetaPath.GetMetaPath(change.Path));
                }

                internal static string BuildCacheKey(ChangeTypes changeTypes, string path)
                {
                    return string.Concat(changeTypes, ":", path);
                }
            }
        }
    }
}
