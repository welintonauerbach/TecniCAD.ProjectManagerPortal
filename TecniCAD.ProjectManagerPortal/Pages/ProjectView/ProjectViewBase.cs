using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using TecniCAD.Models;
using TecniCAD.ProjectManagerPortal.Controller;
using TecniCAD.ProjectManagerPortal.Models;
using TecniCAD.ProjectManagerPortal.Service;
using TecniCAD.ProjectManagerPortal.Services;

namespace TecniCAD.ProjectManagerPortal.Pages.ProjectView
{
    public class ProjectViewBase : ComponentBase
    {
        [Inject]
        protected IMatToaster ShowAlert { get; set; }

        [Inject]
        IConfiguration configuration { get; set; }


        [Parameter]
        public string Id { get; set; }

        protected Project project;
        protected List<ProjectItem> itemlList;

        [Inject]
        protected IProjectService apiProject { get; set; }
        
        [Inject]
        protected IManualService apiManual { get; set; }

        protected ProjectItem projectItem;
        protected List<FileLink> manualList;
        protected EmailContent email;
        protected FileLink FileLinkSelected;

        protected string TextId;
        protected string ManualFileId;
        protected string ManualCode;
        protected string ManualName;
        protected string EmailName;
        protected string EmailAdress;
        protected string EmailText;

        protected Mode modeItem = Mode.None;
        protected Mode modeManual = Mode.None;
        protected Mode modeSelect = Mode.None;
        protected Mode modeEmail = Mode.None;


        protected override async Task OnInitializedAsync()
        {
            await LoadProject();
            await LoadManual();
        }

        protected async Task LoadProject()
        {
            try
            {
                var id = Convert.ToInt32(Id);

                project = await apiProject.GetProject(id).ConfigureAwait(false);
                itemlList = project.Items.OrderBy(x => x.ItemNumber).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected async Task LoadManual()
        {
            try
            {
                manualList = await apiManual.GetManuals().ConfigureAwait(false);
            }
            catch (Exception)
            {

            }

        }

        protected void AddItem()
        {
            modeItem = Mode.Add;
            projectItem = new ProjectItem();
            //projectItem.ProjectId = project.ProjectId;
        }

        //protected async Task SaveItem()
        //{
        //    if (modeItem == Mode.Add)
        //    {
        //        if (FileLinkSelected == null)
        //        {
        //            ShowAlert.Add("Selecione um Manual para Salvar o Item!",MatToastType.Danger);
        //            return;
        //        }
        //        //projectItem.FileLinkCollection.Add(); FileLinkId = FileLinkSelected.FileLinkId;
        //        projectItem.ProjectId = project.ProjectId;
        //        var isSucess = await apiProject.SaveProjectItem(projectItem);

        //        if (isSucess)
        //        {
        //            await ReloadProject();
        //            modeItem = Mode.None;
        //            HideManuals();
        //            projectItem = null;
        //            FileLinkSelected = null;
        //            ShowAlert.Add("Item Adicionado com Sucesso", MatToastType.Success);
        //        }
        //        else
        //        {
        //            ShowAlert.Add("Atenção: O item não foi salvo!",MatToastType.Warning);
        //        }
        //    }

        //    if (modeItem == Mode.Edit)
        //    {
        //        //projectItem.FileLinkId = FileLinkSelected.FileLinkId;
        //        projectItem.ProjectId = project.ProjectId;
        //        var isSucess = await apiProject.UpdateProjectItem(projectItem.ProjectItemId, projectItem);
        //        if (isSucess)
        //        {
        //            modeItem = Mode.None;
        //            HideManuals();
        //            projectItem = null;
        //            FileLinkSelected = null;
        //            ShowAlert.Add("Item salvo com sucesso!",MatToastType.Success);
        //        }
        //    }

        //}

        //private async Task ReloadProject()
        //{
        //    project.Items.Clear();
        //    await LoadProject();
        //}

        //protected void CancelItem()
        //{
        //    projectItem = null;
        //    FileLinkSelected = null;
        //    modeItem = Mode.None;
        //    HideManuals();
        //}

        //protected async Task GetManuals()
        //{
        //    if (manualList == null) { manualList = new List<FileLink>(); }
        //    modeSelect = Mode.Add;
        //    manualList = await apiManual.GetManuals();
        //}

        //protected void HideManuals()
        //{
        //    modeSelect = Mode.None;
        //}

        //protected void EditItem(ProjectItem item)
        //{
        //    projectItem = item;

        //    //if (projectItem.FileLink != null)
        //        //FileLinkSelected = item.FileLink;

        //    modeItem = Mode.Edit;
        //}

        /// <summary>
        /// Insere documentos e dados adicionais sobre o ítem
        /// </summary>
        /// <param name="item"></param>
        //protected void OpenDataView()
        //{
        //    //projectItem = item;

        //    //if (projectItem.FileLink != null)
        //    //    FileLinkSelected = item.FileLink;

        //    //modeItem = Mode.Edit;
        //}

        //protected async Task DuplicateItem(ProjectItem item)
        //{
        //    var duplicateItem = new ProjectItem
        //    {
        //        DocNumber = item.DocNumber,
        //        //FileLinkId = item.FileLinkId,
        //        ItemNumber = item.ItemNumber,
        //        Name = item.Name,
        //        OfNumber = item.OfNumber,
        //        ProjectId = item.ProjectId
        //    };

        //    var isSucess = await apiProject.SaveProjectItem(duplicateItem).ConfigureAwait(false);

        //    if (isSucess)
        //    {
        //        await ReloadProject();
        //    }
        //    else
        //    {
        //        ShowAlert.Add("Atenção: O item não foi salvo!", MatToastType.Warning);
        //    }

        //}

        //protected async Task DeleteItem(int item)
        //{
        //    var isSucess = await apiProject.DeleteProjectItem(item);

        //    if (isSucess)
        //    {
        //        await ReloadProject();
        //        ShowAlert.Add("Item Deletado com sucesso!", MatToastType.Success);
        //    }
        //    else
        //    {
        //        ShowAlert.Add("Problema ao Deletar o item!", MatToastType.Warning);
        //    }
        //}

        protected void ComposeEmail()
        {
            modeEmail = Mode.Add;
            email = new EmailContent();
        }

        //protected string IsManualExist(ProjectItem item)
        //{
        //    if (item == null)
        //    {
        //        return "tc-no-manual";
        //    }
        //    ////if (item.FileLink.Code == "000000")
        //    //{
        //    //    return "tc-no-manual";
        //    //}

        //    return "tc-yes-manual";
        //}

    }
}
