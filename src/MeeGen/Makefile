
EXTRA_DIST =  Makefile

# Warning: This is an automatically generated file, do not edit!

srcdir=.
top_srcdir=..

include $(top_srcdir)/config.make

ifeq ($(CONFIG),DEBUG_X86)
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize- -debug "-define:DEBUG"
ASSEMBLY = bin/Debug/MeeGen.exe
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = winexe
PROJECT_REFERENCES = 
BUILD_DIR = bin/Debug

MEEGEN_EXE_MDB_SOURCE=bin/Debug/MeeGen.exe.mdb
MEEGEN_EXE_MDB=$(BUILD_DIR)/MeeGen.exe.mdb

endif

ifeq ($(CONFIG),RELEASE_X86)
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize-
ASSEMBLY = bin/Release/MeeGen.exe
ASSEMBLY_MDB = 
COMPILE_TARGET = winexe
PROJECT_REFERENCES = 
BUILD_DIR = bin/Release

MEEGEN_EXE_MDB=

endif

AL=al2
SATELLITE_ASSEMBLY_NAME=$(notdir $(basename $(ASSEMBLY))).resources.dll

PROGRAMFILES = \
	$(MEEGEN_EXE_MDB)  

BINARIES = \
	$(MEEGEN)  


RESGEN=resgen2

MEEGEN = $(BUILD_DIR)/meegen

FILES =  \
	AssemblyInfo.cs \
	gtk-gui/generated.cs \
	gtk-gui/MeeGen.ColorSelectButton.cs \
	gtk-gui/MeeGen.InfoDialog.cs \
	gtk-gui/MeeGen.MainWindow.cs \
	Layers/Layer.cs \
	Layers/LayerManager.cs \
	Main.cs \
	UI/ColorSelectButton.cs \
	UI/ExportWizard/ExportWizard.cs \
	UI/ExportWizard/LocalExportWizard.cs \
	UI/ExportWizard/Page.cs \
	UI/ExportWizard/Upload/IUploader.cs \
	UI/ExportWizard/Upload/MeeGoForumUpload.cs \
	UI/ExportWizard/WebExportWizard.cs \
	UI/InfoDialog.cs \
	UI/MainWindow.cs \
	UI/MessageBox.cs \
	UI/MoreMenu.cs \
	UI/WidgetHelper.cs 

DATA_FILES = 

RESOURCES =  \
	../MeeGen.sln,MeeGen.MeeGen.sln \
	gtk-gui/gui.stetic \
	Resources/arrow-left.png,MeeGen.Resources.arrow-left.png \
	Resources/arrow-right.png,MeeGen.Resources.arrow-right.png \
	Resources/arrow-up.png,MeeGen.Resources.arrow-up.png \
	Resources/color-chooser.png,MeeGen.Resources.color-chooser.png \
	Resources/dialog-information.png,MeeGen.Resources.dialog-information.png \
	Resources/dialog-question.png,MeeGen.Resources.dialog-question.png \
	Resources/document-save.png,MeeGen.Resources.document-save.png \
	Resources/folder-remote.png,MeeGen.Resources.folder-remote.png \
	Resources/go-down.png,MeeGen.Resources.go-down.png \
	Resources/go-up.png,MeeGen.Resources.go-up.png \
	Resources/ico-meegen.png,MeeGen.Resources.ico-meegen.png \
	Resources/network.png,MeeGen.Resources.network.png \
	Resources/object-flip-horizontal.png,MeeGen.Resources.object-flip-horizontal.png \
	Resources/object-flip-vertical.png,MeeGen.Resources.object-flip-vertical.png \
	Resources/object-rotate-left.png,MeeGen.Resources.object-rotate-left.png \
	Resources/object-rotate-right.png,MeeGen.Resources.object-rotate-right.png \
	Resources/ratio-custom.png,MeeGen.Resources.ratio-custom.png \
	Resources/ratio-preserve.png,MeeGen.Resources.ratio-preserve.png \
	Resources/small-arrow-right.png,MeeGen.Resources.small-arrow-right.png \
	Resources/user-trash.png,MeeGen.Resources.user-trash.png \
	Resources/web.png,MeeGen.Resources.web.png \
	Resources/Wizard/cafepress-logo.png,MeeGen.Resources.Wizard.cafepress-logo.png \
	Resources/Wizard/gravatar-logo.png,MeeGen.Resources.Wizard.gravatar-logo.png \
	Resources/Wizard/meego-forum-logo.png,MeeGen.Resources.Wizard.meego-forum-logo.png \
	Resources/Wizard/zazzle-logo.png,MeeGen.Resources.Wizard.zazzle-logo.png \
	Resources/zoom-in.png,MeeGen.Resources.zoom-in.png \
	Resources/zoom-out.png,MeeGen.Resources.zoom-out.png 

EXTRAS = \
	Layers \
	UI \
	UI/ExportWizard \
	UI/ExportWizard/Upload \
	meegen.in 

REFERENCES =  \
	Mono.Posix \
	-pkg:glib-sharp-2.0 \
	-pkg:gtk-sharp-2.0 \
	-pkg:mono-cairo \
	-pkg:rsvg2-sharp-2.0 \
	System \
	System.Xml

DLL_REFERENCES = 

CLEANFILES = $(PROGRAMFILES) $(BINARIES) 

#Targets
all: $(ASSEMBLY) $(PROGRAMFILES) $(BINARIES)  $(top_srcdir)/config.make

include $(top_srcdir)/Makefile.include
#include $(srcdir)/custom-hooks.make



$(eval $(call emit-deploy-wrapper,MEEGEN,meegen,x))


$(eval $(call emit_resgen_targets))
$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'


$(ASSEMBLY_MDB): $(ASSEMBLY)
$(ASSEMBLY): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list) $(build_satellite_assembly_list)
	make pre-all-local-hook prefix=$(prefix)
	mkdir -p $(shell dirname $(ASSEMBLY))
	make $(CONFIG)_BeforeBuild
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)
	make $(CONFIG)_AfterBuild
	make post-all-local-hook prefix=$(prefix)

install-local: $(ASSEMBLY) $(ASSEMBLY_MDB)
	make pre-install-local-hook prefix=$(prefix)
	make install-satellite-assemblies prefix=$(prefix)
	mkdir -p '$(DESTDIR)$(libdir)/$(PACKAGE)'
	$(call cp,$(ASSEMBLY),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(ASSEMBLY_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(MEEGEN_EXE_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	mkdir -p '$(DESTDIR)$(bindir)'
	$(call cp,$(MEEGEN),$(DESTDIR)$(bindir))
	make post-install-local-hook prefix=$(prefix)

uninstall-local: $(ASSEMBLY) $(ASSEMBLY_MDB)
	make pre-uninstall-local-hook prefix=$(prefix)
	make uninstall-satellite-assemblies prefix=$(prefix)
	$(call rm,$(ASSEMBLY),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(ASSEMBLY_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(MEEGEN_EXE_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(MEEGEN),$(DESTDIR)$(bindir))
	make post-uninstall-local-hook prefix=$(prefix)

install: install-local
uninstall: uninstall-local
clean: clean-local

include $(top_srcdir)/rules.make
