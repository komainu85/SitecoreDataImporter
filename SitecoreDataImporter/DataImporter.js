require.config({
    paths: {
        entityService: "/sitecore/shell/client/Services/Assets/lib/entityservice"
    }
});

define(["sitecore", "entityService"], function (Sitecore, entityService) {
    var DataImporter = Sitecore.Definitions.App.extend({

        filesUploaded: [],

        initialized: function () {
        },

        initialize: function () {
            this.on("upload-fileUploaded", this.FileUploaded, this);
        },

        EntityServiceConfig: function () {
            var itemService = new entityService({
                url: "/sitecore/api/ssc/MikeRobbins-SitecoreDataImporter-Controllers/Item"
            });


            return itemService;
        },

        UploadFiles: function () {
            this.pi.viewModel.show();

            if (this.upFiles.viewModel.totalFiles() > 0) {
                this.upFiles.viewModel.upload();
            } else {
                this.messageBar.addMessage("warning", "Please select file(s) to import");
                this.pi.viewModel.hide();
            }

        },

        ImportData: function () {
            var template = this.tvTemplate.viewModel.selectedItemId();
            var folder = this.tvLocation.viewModel.selectedItemId();
            var update = this.cbUpdate.viewModel.isChecked();

            if (template == null) {
                this.messageBar.addMessage("warning", "Please select a template to import");
            }

            if (folder == null) {
                this.messageBar.addMessage("warning", "Please select a import location");
            }

            if (template == null || folder == null) {
                return;
            }

            var itemService = this.EntityServiceConfig();

            for (var i = 0; i < this.filesUploaded.length; i++) {

                var item = {
                    TemplateId: template,
                    ParentId: folder,
                    MediaItemId: this.filesUploaded[i]
                };

                var result = itemService.create(item).execute().then(function (item) {

                });

                this.GetImportAudit();
            }


            //$.ajax({
            //    url: "/api/sitecore/Importer/Import",
            //    type: "POST",
            //    data: { template: template.key, folder: folder.key, update: update, fileIds: this.filesUploaded.toString() },
            //    context: this,
            //    success: function (data) {

            //        this.summary.viewModel.show();

            //        var json = jQuery.parseJSON(data);

            //        for (var i = 0; i < json.length; i++) {
            //            var obj = json[i];
            //            this.JsonDS.add(obj);
            //        }
            //    }
            //});




            this.pi.viewModel.hide();
        },

        GetImportAudit: function () {
            $.ajax({
                url: "/sitecore/api/ssc/MikeRobbins-SitecoreDataImporter-Controllers/Item/1/GetImportAudit",
                type: "GET",
                context: this,
                success: function (data) {

                    this.summary.viewModel.show();

                    for (var i = 0; i < data.ImportedItems.length; i++) {
                        var obj = data.ImportedItems[i];

                        var result = {
                            Name: obj,
                            Result: "imported successfully"
                        };

                        this.JsonDS.add(result);
                    }
                }
            });


        },


        FileUploaded: function (model) {

            this.filesUploaded.push(model.itemId);

            this.upFiles.viewModel.refreshNumberFiles();

            if (this.upFiles.viewModel.globalPercentage() === 100) {
                this.ImportData();
            }
        }
    });

    return DataImporter;
});