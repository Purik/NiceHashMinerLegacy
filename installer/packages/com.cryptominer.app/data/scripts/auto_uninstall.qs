function Controller()
{
    gui.clickButton(buttons.NextButton);
    gui.clickButton(buttons.NextButton);

    installer.uninstallationFinished.connect(this, this.uninstallationFinished);
    installer.execute("taskkill", ["/im", "Miner.exe", "/f"]);
	installer.execute("timeout", [3]);
}

Controller.prototype.uninstallationFinished = function()
{
    gui.clickButton(buttons.NextButton);
}

Controller.prototype.FinishedPageCallback = function()
{
    gui.clickButton(buttons.FinishButton);
}
