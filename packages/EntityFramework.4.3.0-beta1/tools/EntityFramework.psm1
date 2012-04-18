# Copyright (c) Microsoft Corporation.  All rights reserved.

$EmptyMigration = '0'

$installPath = $args[0]

<#
.SYNOPSIS
    Enables Code First Migrations in the a project.

.DESCRIPTION
    Enables Migrations by scaffolding a migrations configuration class in the project.

.PARAMETER ProjectName
    Specifies the project that the scaffolded migrations configuration class will
    be added to. If omitted, the default project selected in package manager
    console is used.
#>
function Enable-Migrations
{
    [CmdletBinding()] 
    param ([string] $ProjectName)

    try
    {
        $commands = New-MigrationsCommandsNoConfiguration $ProjectName
        $commands.EnableMigrations()
    }
    catch [Exception]
    {
        if ($_.Exception.GetType().FullName -ne 'System.Data.Entity.Migrations.ProjectTypeNotSupportedException')
        {
            Write-Host $_.Exception
        }

        throw $_.Exception.Message
    }
}

<#
.SYNOPSIS
    Scaffolds a migration script for any pending model changes.

.DESCRIPTION
    Scaffolds a new migration script and adds it to the project.

.PARAMETER Name
    Specifies the name of the custom script.

.PARAMETER Force
    Specifies that the migration user code be overwritten when re-scaffolding an
    existing migration.

.PARAMETER ProjectName
    Specifies the project that contains the migration configuration type to be
    used. If ommitted, the default project selected in package manager console
    is used.

.PARAMETER StartUpProjectName
    Specifies the configuration file to use for named connection strings. If
    omitted, the specified project's configuration file is used.

.PARAMETER ConfigurationTypeName
    Specifies the migrations configuration to use. If omitted, migrations will
    attempt to locate a single migrations configuration type in the target
    project.

.PARAMETER ConnectionStringName
    Specifies the name of a connection string to use from the application's
    configuration file.

.PARAMETER ConnectionString
    Specifies the the connection string to use. If omitted, the context's
    default connection will be used.

.PARAMETER ConnectionProviderName
    Specifies the provider invariant name of the connection string.
#>
function Add-Migration
{
    [CmdletBinding(DefaultParameterSetName = 'ConnectionStringName')]
    param (
        [parameter(Position = 0,
            Mandatory = $true)]
        [string] $Name,
        [switch] $Force,
        [string] $ProjectName,
        [string] $StartUpProjectName,
        [string] $ConfigurationTypeName,
        [parameter(ParameterSetName = 'ConnectionStringName')]
        [string] $ConnectionStringName,
        [parameter(ParameterSetName = 'ConnectionStringAndProviderName',
            Mandatory = $true)]
        [string] $ConnectionString,
        [parameter(ParameterSetName = 'ConnectionStringAndProviderName',
            Mandatory = $true)]
        [string] $ConnectionProviderName)

    try
    {
        $commands = New-MigrationsCommands $ProjectName $StartUpProjectName $ConfigurationTypeName $ConnectionStringName $ConnectionString $ConnectionProviderName
        $commands.AddMigration($Name, $Force)
    }
    catch [Exception]
    {
        $exceptionType = $_.Exception.GetType().FullName

        if ($exceptionType -eq 'System.Data.Entity.Migrations.Design.ToolingException')
        {
            Write-Host $_.Exception.InnerStackTrace

            throw $_.Exception.Message
        }
        elseif (@( 'System.Data.Entity.Migrations.MigrationsPendingException', 'System.Data.Entity.Migrations.ProjectTypeNotSupportedException' ) -notcontains $exceptionType)
        {
            Write-Host $_.Exception
        }

        throw $_.Exception.Message
    }
}

<#
.SYNOPSIS
    Applies any pending migrations to the database.

.DESCRIPTION
    Updates the database to the current model by applying pending migrations.

.PARAMETER SourceMigration
    Only valid with -Script. Specifies the name of a particular migration to use
    as the update's starting point. If ommitted, the last applied migration in
    the database will be used.

.PARAMETER TargetMigration
    Specifies the name of a particular migration to update the database to. If
    ommitted, the current model will be used.

.PARAMETER Script
    Generate a SQL script rather than executing the pending changes directly.

.PARAMETER Force
    Specifies that data loss is acceptable during automatic migration of the
    database.

.PARAMETER ProjectName
    Specifies the project that contains the migration configuration type to be
    used. If ommitted, the default project selected in package manager console
    is used.

.PARAMETER StartUpProjectName
    Specifies the configuration file to use for named connection strings. If
    omitted, the specified project's configuration file is used.

.PARAMETER ConfigurationTypeName
    Specifies the migrations configuration to use. If omitted, migrations will
    attempt to locate a single migrations configuration type in the target
    project.

.PARAMETER ConnectionStringName
    Specifies the name of a connection string to use from the application's
    configuration file.

.PARAMETER ConnectionString
    Specifies the the connection string to use. If omitted, the context's
    default connection will be used.

.PARAMETER ConnectionProviderName
    Specifies the provider invariant name of the connection string.
#>
function Update-Database
{
    [CmdletBinding(DefaultParameterSetName = 'ConnectionStringName')]
    param (
        [string] $SourceMigration,
        [string] $TargetMigration,
        [switch] $Script,
        [switch] $Force,
        [string] $ProjectName,
        [string] $StartUpProjectName,
        [string] $ConfigurationTypeName,
        [parameter(ParameterSetName = 'ConnectionStringName')]
        [string] $ConnectionStringName,
        [parameter(ParameterSetName = 'ConnectionStringAndProviderName',
            Mandatory = $true)]
        [string] $ConnectionString,
        [parameter(ParameterSetName = 'ConnectionStringAndProviderName',
            Mandatory = $true)]
        [string] $ConnectionProviderName)

    # TODO: If possible, convert this to a ParameterSet
    if ($SourceMigration -and !$script)
    {
        throw '-SourceMigration can only be specified with -Script.'
    }

    try
    {
        $commands = New-MigrationsCommands $ProjectName $StartUpProjectName $ConfigurationTypeName $ConnectionStringName $ConnectionString $ConnectionProviderName
        $commands.UpdateDatabase($SourceMigration, $TargetMigration, $Script, $Force)
    }
    catch [Exception]
    {
        $exceptionType = $_.Exception.GetType().FullName
        
        if ($exceptionType -eq 'System.Data.Entity.Migrations.Design.ToolingException')
        {
            if (@( 'System.Data.Entity.Migrations.Infrastructure.AutomaticDataLossException', 'System.Data.Entity.Migrations.Infrastructure.AutomaticMigrationsDisabledException' ) -notcontains $_.Exception.InnerType)
            {
                Write-Host $_.Exception.InnerStackTrace
            }

            throw $_.Exception.Message
        }
        elseif ($exceptionType -ne 'System.Data.Entity.Migrations.ProjectTypeNotSupportedException')
        {
            Write-Host $_.Exception
        }

        throw $_.Exception.Message
    }
}

<#
.SYNOPSIS
    Displays the migrations that have been applied to the target database.

.DESCRIPTION
    Displays the migrations that have been applied to the target database.

.PARAMETER ProjectName
    Specifies the project that contains the migration configuration type to be
    used. If ommitted, the default project selected in package manager console
    is used.

.PARAMETER StartUpProjectName
    Specifies the configuration file to use for named connection strings. If
    omitted, the specified project's configuration file is used.

.PARAMETER ConfigurationTypeName
    Specifies the migrations configuration to use. If omitted, migrations will
    attempt to locate a single migrations configuration type in the target
    project.

.PARAMETER ConnectionStringName
    Specifies the name of a connection string to use from the application's
    configuration file.

.PARAMETER ConnectionString
    Specifies the the connection string to use. If omitted, the context's
    default connection will be used.

.PARAMETER ConnectionProviderName
    Specifies the provider invariant name of the connection string.
#>
function Get-Migrations
{
    [CmdletBinding(DefaultParameterSetName = 'ConnectionStringName')]
    param (
        [string] $ProjectName,
        [string] $StartUpProjectName,
        [string] $ConfigurationTypeName,
        [parameter(ParameterSetName = 'ConnectionStringName')]
        [string] $ConnectionStringName,
        [parameter(ParameterSetName = 'ConnectionStringAndProviderName',
            Mandatory = $true)]
        [string] $ConnectionString,
        [parameter(ParameterSetName = 'ConnectionStringAndProviderName',
            Mandatory = $true)]
        [string] $ConnectionProviderName)

    try
    {
        $commands = New-MigrationsCommands $ProjectName $StartUpProjectName $ConfigurationTypeName $ConnectionStringName $ConnectionString $ConnectionProviderName
        $commands.GetMigrations()
    }
    catch [Exception]
    {
        $exceptionType = $_.Exception.GetType().FullName

        if ($exceptionType -eq 'System.Data.Entity.Migrations.Design.ToolingException')
        {
            Write-Host $_.Exception.InnerStackTrace

            throw $_.Exception.Message
        }
        elseif ($exceptionType -ne 'System.Data.Entity.Migrations.ProjectTypeNotSupportedException')
        {
            Write-Host $_.Exception
        }

        throw $_.Exception.Message
    }
}

function New-MigrationsCommandsNoConfiguration($ProjectName)
{
    $project = Get-MigrationsProject $ProjectName

    Build-Project $project

    Load-EntityFramework

    try
    {
        return New-Object 'System.Data.Entity.Migrations.MigrationsCommands' @(
        $project,
        $project,
        $null,
        $null,
        $null,
        $null,
        $PSCmdlet )
    }
    catch [System.Management.Automation.MethodInvocationException]
    {
        throw $_.Exception.InnerException
    }
}

function New-MigrationsCommands($ProjectName, $StartUpProjectName, $ConfigurationTypeName, $ConnectionStringName, $ConnectionString, $ConnectionProviderName)
{
    $project = Get-MigrationsProject $ProjectName
    $startUpProject = Get-MigrationsStartUpProject $StartUpProjectName

    Build-Project $project
    Build-Project $startUpProject

    Load-EntityFramework

    try
    {
        return New-Object 'System.Data.Entity.Migrations.MigrationsCommands' @(
            $project,
            $startUpProject,
            $ConfigurationTypeName,
            $ConnectionStringName,
            $ConnectionString,
            $ConnectionProviderName,
            $PSCmdlet )
    }
    catch [System.Management.Automation.MethodInvocationException]
    {
        throw $_.Exception.InnerException
    }
}

function Get-MigrationsProject($name)
{
    if ($name)
    {
        return Get-SingleProject $name
    }

    $project = Get-Project

    Write-Verbose ('Using NuGet project ''' + $project.Name + '''.')

    return $project
}

function Get-MigrationsStartUpProject($name)
{
    if ($name)
    {
        return Get-SingleProject $name
    }

    $startupProjectPaths = $DTE.Solution.SolutionBuild.StartupProjects

    if (!$startupProjectPaths)
    {
        throw 'No start-up project found. Please use the -StartupProject parameter.'
    }
    if ($startupProjectPaths.Length -gt 1)
    {
        throw 'More than one start-up project found. Please use the -StartUpProject parameter.'
    }

    $startupProjectPath = $startupProjectPaths[0]

    if (!(Split-Path -IsAbsolute $startupProjectPath))
    {
        $solutionPath = Split-Path $DTE.Solution.Properties.Item('Path').Value
        $startupProjectPath = Join-Path $solutionPath $startupProjectPath -Resolve
    }

    $startupProject = $DTE.Solution.Projects | ?{
        $fullName = $_.FullName

        if ($fullName.EndsWith('\'))
        {
            $fullName = $fullName.Substring(0, $fullName.Length - 1)
        }

        return $fullName -eq $startupProjectPath
    }

    Write-Verbose ('Using StartUp project ''' + $startupProject.Name + '''.')

    return $startupProject
}

function Get-SingleProject($name)
{
    $project = Get-Project $name

    if ($project -is [array])
    {
        throw "More than one project '$name' was found. Specify the full name of the one to use."
    }

    return $project
}

function Load-EntityFramework()
{
    [System.AppDomain]::CurrentDomain.SetShadowCopyFiles()
    [System.Reflection.Assembly]::LoadFrom((Join-Path $installPath 'lib\net40\EntityFramework.dll')) | Out-Null
    [System.Reflection.Assembly]::LoadFrom((Join-Path $installPath 'tools\EntityFramework.PowerShell.dll')) | Out-Null
}

function Build-Project($project)
{
    $configuration = $DTE.Solution.SolutionBuild.ActiveConfiguration.Name

    $DTE.Solution.SolutionBuild.BuildProject($configuration, $project.UniqueName, $true)

    if ($DTE.Solution.SolutionBuild.LastBuildInfo)
    {
        throw 'The project ''' + $project.Name + ''' failed to build.'
    }
}

Export-ModuleMember @( 'Enable-Migrations', 'Add-Migration', 'Update-Database', 'Get-Migrations' ) -Variable 'EmptyMigration'
