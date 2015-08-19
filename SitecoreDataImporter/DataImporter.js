define(["sitecore"], function (Sitecore) {
    var DataImporter = Sitecore.Definitions.App.extend({

        filesUploaded: [],

        initialize: function () {
            this.on("upload-fileUploaded", this.FileUploaded, this);
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

            for (var i = 0; i < this.filesUploaded.length; i++) {

                var item = {
                    TemplateId: template,
                    ParentId: folder,
                    MediaItemId: this.filesUploaded[i]
                };

                $.ajax({
                    url: "/sitecore/api/ssc/MikeRobbins-SitecoreDataImporter-Controllers/Item/1/ImportItems",
                    type: "PUT",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify(item)
                });

                this.GetImportAudit(this.filesUploaded[i]);
            }


            this.pi.viewModel.hide();
        },

        GetImportAudit: function (mediaItemId) {
            $.ajax({
                url: "/sitecore/api/ssc/MikeRobbins-SitecoreDataImporter-Controllers/Item/"+ mediaItemId+"/GetImportAudit",
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