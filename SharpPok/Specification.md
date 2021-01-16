# Pok Package Manager Specification
### V1.0.0-alpha.1 DRAFT001

## Introduction
This file contains the specification for Chroma Subsystem #15. The subsystem is responsible for managing packages, this contains downloading and searching.
This is the first draft of a standardisation effort to ensure the **POK** package managers longevity both as part of the Chroma operating system and outside of it as a general purpose package manager. 
The standard defines the behaviour of both the client and server side but leaves the implementation details to the programmers.
the goal of this document is to make sure the interactions between the components is properly defined and agreed upon.

## The POK-API
The POK-API can be thought of as the version of this specification, that the server or client is conforms to. The version identifier must strictly follow [Semantic Versioning 2.0.0](https://semver.org/), allowing for ``-alpha.1`` and ``-beta.1`` suffixes as per the [Sem Ver Specification §9](https://semver.org/#spec-item-9).

## Packages

A package is comprised of two main parts:
1) The payload that is the core of the package. This can be many things in most cases the compiled code of a program. But any file can be thought of as a package.
2) The package Metadata. this describes the package and gives extra information about it.

### Package Metadata
The package metadata as of this version (V1.0.0.-alpha.1) contains the following data 

Properties:

| Name   | Definition | Pattern |
|--------|------------| ------- |
| Package ID | The unique ID of the package | [a-z0-9.]
| Author     | The author of the package    | [A-Za-z0-9]
| Name       | The name of the package      | [A-Za-z0-9]

#### Package ID
The package ID is a unique identifier for the package, and is derived form the ``Author`` and the ``Name`` metadata. The id is constructed as follows ``<Author>.<Name>`` for example: ``curle.erythro``.
While the ``Author`` and ``Name`` fields can contain upper and lower case letters the package ID must always use their lover case variants.

#### Author
This denotes the author of the package and can use alpha numeric character, but can't contain ``.``.

#### Name
This denotes the name of the package and can use alpha numeric character, but can't contain ``.``.
The usage of ``.`` is restricted for future possibility of sub packages or indicating package relations.

## The Server
The server is responsible for storing a database of all the packages and versions 
The server must expose the following api: