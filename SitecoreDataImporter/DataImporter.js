define(["sitecore"], function (Sitecore) {
    var DataImporter = Sitecore.Definitions.App.extend({

        filesUploaded: [],

        initialized: function () { },

        initialize: function () {

            var fileUploaded = function (model) {
                this.filesUploaded.push(model.itemId);

                this.upFiles.viewModel.refreshNumberFiles();

                if (this.upFiles.viewModel.globalPercentage() == 100) {

                    if (this.upFiles.viewModel.totalFiles() == 1) {
                           this.ImportData();
                    }
                }
            };

            this.on("upload-fileUploaded", fileUploaded, this);

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
            var template = this.tvTemplate.viewModel.selectedNode();
            var folder = this.tvLocation.viewModel.selectedNode();
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

            $.ajax({
                url: "/api/sitecore/Importer/Import",
                type: "POST",
                data: { template: template.key, folder: folder.key, update: update, fileIds: this.filesUploaded.toString() },
                context: this,
                success: function (data) {

                    this.summary.viewModel.show();

                    var json = jQuery.parseJSON(data);

                    for (var i = 0; i < json.length; i++) {
                        var obj = json[i];

                        console.log(obj.result);

                        this.JsonDS.add(obj);

                    }
                }
            });
            this.pi.viewModel.hide();
        },
    });

    return DataImporter;
});