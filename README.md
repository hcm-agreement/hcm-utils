
<div align="center">
<h3 align="center">@hcm-agreement/hcm-utils</h3>
<p align="center">
A .NET library for dealing with HCM input and output data

<a href="https://hcm-agreement.github.io/hcm-utils"><strong>Explore the docs »</strong></a>
<br />
<a href="https://github.com/hcm-agreement/hcm-utils/issues/new">Report Issue</a>
</p>
</div>

![Workflow Status](https://github.com/hcm-agreement/hcm-utils/actions/workflows/main.yml/badge.svg)
![License](https://img.shields.io/github/license/hcm-agreement/hcm-utils)
![NuGet Version](https://img.shields.io/nuget/hcm-agreement/hcm-utils)


# Getting Started

## Installation

```bash
dotnet add package HCMAgreement.HCMUtils
```

The package is supported on Linux and Windows with a minimum .NET SDK of 10.0.

## Examples

### Build a legacy string

Build an input string for a point-to-line calculation for the legacy API:

```cs
var inputOutputString = HCMUtils.String.Helpers.BuildLegacyInputString(
    TxCoordinates,
    TxSiteHeight,
    TxAntennaType,
    TxAzimuth,
    TxElevation,
    TxAntennaHeight,
    TxGainType,
    TxPower,
    TxFrequency,
    ChannelOccupation,
    SeaTemperature,
    TxServiceAreaRadius,
    DistanceOverSea,
    TxEmissionDesignation,
    PermissibleFieldStrength,
    TargetCountry,
    TxCountry,
    MaxCrossBorderRange,
    "D:\\TOPO",
    "D:\\BORDER",
    "D:\\MORPHO"
);
```

### Convert HCM input strings

```cs
var frequency = StringHelpers.ParseSINumber("3.8G"); // => 3_800_000_000
var gainType = StringHelpers.ParseGainType("E"); // => GainType.Dipole
// ...
```

# Features

* convert to and from (legacy) HCM input data
* build input strings for the legacy HCM interface
* *platform-agnostic* - use anywhere!

# Contributing

Please refer to [the contributing guide](https://github.com/hcm-agreement/hcm-utils/blob/main/CONTRIBUTING.md).

# Contributors

<a href="https://github.com/hcm-agreement/hcm-utils/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=hcm-agreement/hcm-utils" />
</a>

Made with [contrib.rocks](https://contrib.rocks).
