﻿using SolidEdgeCommunity.Extensions; // Enabled extension methods from SolidEdge.Community.dll
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ApiDemos.Draft
{
    /// <summary>
    /// Copies the 1st parts list of the active draft to the clipboard and pastes it into Excel.
    /// </summary>
    class CopyPartsListsToClipboardAndPasteIntoExcel
    {
        static void RunSample(bool breakOnStart)
        {
            if (breakOnStart) System.Diagnostics.Debugger.Break();

            SolidEdgeFramework.Application application = null;
            SolidEdgeDraft.DraftDocument draftDocument = null;
            SolidEdgeDraft.PartsLists partsLists = null;
            SolidEdgeDraft.PartsList partsList = null;
            dynamic excelApplication = null;
            dynamic excelWorkbooks = null;
            dynamic excelWorkbook = null;
            dynamic excelWorksheet = null;

            try
            {
                // Register with OLE to handle concurrency issues on the current thread.
                SolidEdgeCommunity.OleMessageFilter.Register();

                // Connect to or start Solid Edge.
                application = SolidEdgeCommunity.SolidEdgeUtils.Connect(false);

                // Get a reference to the active draft document.
                draftDocument = application.GetActiveDocument<SolidEdgeDraft.DraftDocument>(false);

                if (draftDocument != null)
                {
                    // Get a reference to the PartsLists collection.
                    partsLists = draftDocument.PartsLists;

                    if (partsLists.Count > 0)
                    {
                        // Get a reference to the 1st parts list.
                        partsList = partsLists.Item(1);

                        // Copy parts list to clipboard.
                        partsList.CopyToClipboard();

                        try
                        {
                            excelApplication = Marshal.GetActiveObject("Excel.Application");
                        }
                        catch
                        {
                            excelApplication = Activator.CreateInstance(Type.GetTypeFromProgID("Excel.Application"));
                        }

                        if (excelApplication != null)
                        {
                            excelApplication.Visible = true;
                            excelWorkbooks = excelApplication.Workbooks;
                            excelWorkbook = excelWorkbooks.Add();
                            excelWorksheet = excelWorkbook.ActiveSheet;
                            excelWorksheet.Paste();
                        }
                    }
                    else
                    {
                        throw new System.Exception(Resources.NoPartsListsInDraftDocument);
                    }
                }
                else
                {
                    throw new System.Exception(Resources.NoActiveDraftDocument);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                SolidEdgeCommunity.OleMessageFilter.Unregister();
            }
        }
    }
}
