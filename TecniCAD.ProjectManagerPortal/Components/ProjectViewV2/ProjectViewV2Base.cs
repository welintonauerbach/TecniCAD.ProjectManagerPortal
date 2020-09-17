﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using TecniCAD.Models;
using TecniCAD.ProjectManagerPortal.Controller;
using TecniCAD.ProjectManagerPortal.Models;
using TecniCAD.ProjectManagerPortal.Service;
using TecniCAD.ProjectManagerPortal.Services;

namespace TecniCAD.ProjectManagerPortal.Components.ProjectViewV2
{
    public class ProjectViewV2Base : ComponentBase
    {
        [Inject] IJSRuntime js { get; set; }

        [Inject] IConfiguration configuration { get; set; }

        [Parameter] public string Id { get; set; }

        protected Project project;
        protected List<ProjectItem> itemlList;

        protected ProjectService apiProject { get; set; }
        protected ProjectItem projectItem;
        protected ProjectItem projectItemSelected;


        protected List<FileLink> manualList;
        protected ManualService apiManual;
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

        protected void OnSelectItemChange(ProjectItem item)
        {
            projectItemSelected = item;
        }
    



    protected override async Task OnInitializedAsync()
        {
            apiProject = new ProjectService(configuration);
            apiManual = new ManualService(configuration);
            await LoadProject();
            await LoadManual();
        }

        protected async Task LoadProject()
        {
            try
            {
                var id = Convert.ToInt32(Id);

                project = await apiProject.GetProject(id);
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
                manualList = await apiManual.GetManuals();
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

        protected async Task SaveItem()
        {
            if (modeItem == Mode.Add)
            {
                if (FileLinkSelected == null)
                {
                    await Alert("Selecione um Manual para Salvar o Item!");
                    return;
                }
                projectItem.FileLinkId = FileLinkSelected.FileLinkId;
                projectItem.ProjectId = project.ProjectId;
                var isSucess = await apiProject.SaveProjectItem(projectItem);

                if (isSucess)
                {
                    await ReloadProject();
                    modeItem = Mode.None;
                    HideManuals();
                    projectItem = null;
                    FileLinkSelected = null;
                }
                else
                {
                    await Alert("Atenção: O item não foi salvo!");
                }
            }

            if (modeItem == Mode.Edit)
            {
                projectItem.FileLinkId = FileLinkSelected.FileLinkId;
                projectItem.ProjectId = project.ProjectId;
                var isSucess = await apiProject.UpdateProjectItem(projectItem.ProjectItemId, projectItem);
                if (isSucess)
                {
                    modeItem = Mode.None;
                    HideManuals();
                    projectItem = null;
                    FileLinkSelected = null;
                    await Alert("Item salvo com sucesso!");
                }
            }

        }

        private async Task ReloadProject()
        {
            project.Items.Clear();
            await LoadProject();
        }

        protected void CancelItem()
        {
            projectItem = null;
            FileLinkSelected = null;
            modeItem = Mode.None;
            HideManuals();
        }

        protected async Task GetManuals()
        {
            if (manualList == null) { manualList = new List<FileLink>(); }
            modeSelect = Mode.Add;
            manualList = await apiManual.GetManuals();
        }

        protected void HideManuals()
        {
            modeSelect = Mode.None;
        }

        protected void EditItem(ProjectItem item)
        {
            projectItem = item;

            if (projectItem.FileLink != null)
                FileLinkSelected = item.FileLink;

            modeItem = Mode.Edit;
        }

        /// <summary>
        /// Insere documentos e dados adicionais sobre o ítem
        /// </summary>
        /// <param name="item"></param>
        protected void OpenDataView()
        {
            //projectItem = item;

            //if (projectItem.FileLink != null)
            //    FileLinkSelected = item.FileLink;

            //modeItem = Mode.Edit;
        }

        protected async Task DuplicateItem(ProjectItem item)
        {
            var duplicateItem = new ProjectItem
            {
                DocNumber = item.DocNumber,
                FileLinkId = item.FileLinkId,
                ItemNumber = item.ItemNumber,
                Name = item.Name,
                OfNumber = item.OfNumber,
                ProjectId = item.ProjectId
            };

            var isSucess = await apiProject.SaveProjectItem(duplicateItem);

            if (isSucess)
            {
                await ReloadProject();
            }
            else
            {
                await Alert("Atenção: O item não foi salvo!");
            }

        }

        protected async Task DeleteItem(int item)
        {
            var isSucess = await apiProject.DeleteProjectItem(item);

            if (isSucess)
            {
                await ReloadProject();
                //await js.InvokeAsync<object>("ShowAlert", "Item Deletado com sucesso!");
            }
            else
            {
                await Alert("Problema ao Deletar o item!");
            }
        }

        private async Task Alert(string msg)
        {
            await js.InvokeAsync<object>("ShowAlert", msg);
        }

        protected void ComposeEmail()
        {
            modeEmail = Mode.Add;
            email = new EmailContent();
        }
        
        protected void CancelComposeEmail()
        {
            modeEmail = Mode.None;
            email = null;
        }

        protected string destinataries;

        protected async Task SendEmail()
        {
            if (email != null)
            {
                var emails = destinataries.Split(';');
                email.ToAdress = emails;

                if (string.IsNullOrEmpty(email.ToName) || email.ToAdress.Length == 0)
                {
                    return;
                }

                email.ProjectNumber = project.ProjectNumber;
                email.Subject = $"Documentos Projeto: {project.ProjectNumber}";
                email.ProjectList = project.Items.ToList().OrderBy(o => o.ItemNumber).ToList();
                email.FromAdress = "cad@idugel.com.br";
                email.FromName = "Grupo Idugel";                
            }          
            
            var isSucess = await apiProject.SendEmail(email).ConfigureAwait(false);

            if (isSucess)
            {                
                await Alert("Email enviado com sucesso!");
                modeEmail = Mode.None;
            }
            else
            {
                await Alert("Falha ao enviar o Email");
            }

        }

        protected string IsManualExist(ProjectItem item)
        {
            if (item == null)
            {
                return "tc-no-manual";
            }
            if (item.FileLink.Code == "000000")
            {
                return "tc-no-manual";
            }

            return "tc-yes-manual";
        }

    }
}
