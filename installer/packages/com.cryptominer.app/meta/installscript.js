
var ComponentSelectionPage = null;
var targetDir = null;
var antivirusHelpURL = null;

var Dir = new function () {
    this.toNativeSparator = function (path) {
        if (systemInfo.productType === "windows")
            return path.replace(/\//g, '\\');
        return path;
    }
};

function Component() {
	installer.gainAdminRights();
	targetDir = Dir.toNativeSparator(installer.value("TargetDir"));
    if (installer.isInstaller()) {
        component.loaded.connect(this, Component.prototype.installerLoaded);
        ComponentSelectionPage = gui.pageById(QInstaller.ComponentSelection);

        installer.setDefaultPageVisible(QInstaller.TargetDirectory, false);
        installer.setDefaultPageVisible(QInstaller.ComponentSelection, false);
        installer.setDefaultPageVisible(QInstaller.LicenseCheck, false);
        if (systemInfo.productType === "windows")
            installer.setDefaultPageVisible(QInstaller.StartMenuSelection, false);
        installer.setDefaultPageVisible(QInstaller.ReadyForInstallation, false);
		
		if (installer.fileExists(targetDir)) {
			installer.execute("mkdir", targetDir);
		}
    }
}

Component.prototype.installerLoaded = function () {
    if (installer.addWizardPage(component, "TargetWidget", QInstaller.TargetDirectory)) {
        var widget = gui.pageWidgetByObjectName("DynamicTargetWidget");
        if (widget != null) {
            widget.targetChooser.clicked.connect(this, Component.prototype.chooseTarget);
            widget.targetDirectory.textChanged.connect(this, Component.prototype.targetChanged);

            widget.windowTitle = "Выберите каталог установки";
            widget.targetDirectory.text = Dir.toNativeSparator(installer.value("TargetDir"));
        }
    }

    if (installer.addWizardPage(component, "InstallationWidget", QInstaller.ComponentSelection)) {
        var widget = gui.pageWidgetByObjectName("DynamicInstallationWidget");
        if (widget != null) {
            widget.kasperskyInstall.toggled.connect(this, Component.prototype.kasperskyInstallToggled);
            widget.drwebInstall.toggled.connect(this, Component.prototype.drwebInstallToggled);
            widget.nortonInstall.toggled.connect(this, Component.prototype.nortonInstallToggled);
			widget.mcafeeInstall.toggled.connect(this, Component.prototype.mcafeeInstallToggled);
			widget.avastInstall.toggled.connect(this, Component.prototype.avastInstallToggled);

            widget.kasperskyInstall.checked = true;
			Component.prototype.kasperskyInstallToggled(true);
            widget.windowTitle = "Выберите Ваш антивирус";
        }

        if (installer.addWizardPage(component, "LicenseWidget", QInstaller.LicenseCheck)) {
            var widget = gui.pageWidgetByObjectName("DynamicLicenseWidget");
            if (widget != null) {
                widget.acceptLicense.toggled.connect(this, Component.prototype.checkAccepted);

                widget.complete = false;
                widget.declineLicense.checked = true;
                widget.windowTitle = "Настройте Ваш антивирус для каталога " + "<span style='color:blue;'>" + targetDir + "</span>";
				var page = gui.pageByObjectName("DynamicLicenseWidget");
				if (page != null) {
					page.entered.connect(this, Component.prototype.licenseWidgetEntered);
				}
            }
        }
		
        if (installer.addWizardPage(component, "ReadyToInstallWidget", QInstaller.ReadyForInstallation)) {
            var widget = gui.pageWidgetByObjectName("DynamicReadyToInstallWidget");
            if (widget != null) {
                //widget.showDetails.checked = false;
                //widget.windowTitle = "Ready to Install";
            }
            var page = gui.pageByObjectName("DynamicReadyToInstallWidget");
            if (page != null) {
                page.entered.connect(this, Component.prototype.readyToInstallWidgetEntered);
            }
        }
		
    }
}

Component.prototype.targetChanged = function (text) {
    var widget = gui.pageWidgetByObjectName("DynamicTargetWidget");
    if (widget != null) {
        if (text != "") {
			widget.complete = true;
			installer.setValue("TargetDir", text);
			return;
        }
        widget.complete = false;
    }
}

Component.prototype.chooseTarget = function () {
    var widget = gui.pageWidgetByObjectName("DynamicTargetWidget");
    if (widget != null) {
        var newTarget = QFileDialog.getExistingDirectory("Выберите каталог установки.", widget
            .targetDirectory.text);
        if (newTarget != "")
            widget.targetDirectory.text = Dir.toNativeSparator(newTarget);
    }
}

Component.prototype.kasperskyInstallToggled = function (checked) {
	if (checked){
		antivirusHelpURL = "https://support.kaspersky.ru/12987";
	}
}

Component.prototype.drwebInstallToggled = function (checked) {
    if (checked){
		antivirusHelpURL = "https://support.drweb.ru/show_faq/?question=5440&lng=ru";
	}
}

Component.prototype.nortonInstallToggled = function (checked) {
    if (checked){
		antivirusHelpURL = "https://www.youtube.com/watch?v=4lQMiSMh2J0";
	}
}

Component.prototype.mcafeeInstallToggled = function (checked) {
	if (checked){
		antivirusHelpURL = "https://chopen.net/antivirus-mcafee-kak-dobavit-fayl-v-isklyucheniya/";
	}
}

Component.prototype.avastInstallToggled = function (checked) {
	if (checked){
		antivirusHelpURL = "https://blog.avast.com/ru/kak-dobavit-isklyucheniya-v-antivirus-avast";
	}
}

Component.prototype.checkAccepted = function (checked) {
    var widget = gui.pageWidgetByObjectName("DynamicLicenseWidget");
    if (widget != null)
        widget.complete = checked;
}

Component.prototype.readyToInstallWidgetEntered = function () {
	var widget = gui.pageWidgetByObjectName("DynamicReadyToInstallWidget");
    if (widget != null) {
        
    }
}

Component.prototype.licenseWidgetEntered = function() {
	QDesktopServices.openUrl(antivirusHelpURL);
}
