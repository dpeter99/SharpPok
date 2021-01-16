# Pok Package Manager Specification
### V1.0.0-alpha.1 DRAFT001

## 1 Introduction
This file contains the specification for Chroma Subsystem #15. The subsystem is responsible for managing packages, this contains downloading and searching.
This is the first draft of a standardisation effort to ensure the **POK** package managers longevity both as part of the Chroma operating system and outside of it as a general purpose package manager. 
The standard defines the behaviour of both the client and server side but leaves the implementation details to the programmers.
the goal of this document is to make sure the interactions between the components is properly defined and agreed upon.

## 2 The POK-API
The POK-API can be thought of as the version of this specification, that the server or client is conforms to. The version identifier must strictly follow [Semantic Versioning 2.0.0](https://semver.org/), allowing for ``-alpha.1`` and ``-beta.1`` suffixes as per the [Sem Ver Specification §9](https://semver.org/#spec-item-9).

## 3 Components
The POK ecosystem is comprised of the server and the client. 

### Client
The client can be any graphical or terminal application that allows the user to acquiere pckage info and payload form a server

### Server
The server is responsible for storing the package metadata and the payloads, and to serve them to the client through the POK-API.



## 4 Packages

A package is comprised of the following main parts:
1) Payload that is the core of the package. This can be many things in most cases the compiled code of a program. But any file can be thought of as a package.
2) Package Metadata. this describes the package and gives extra information about it.

Payloads are organized into [versions (3.2)](#3.2 Package Versions) witch 

### 4.1 Package Metadata
The package metadata as of this version (V1.0.0.-alpha.1) contains the following data 

Properties:

| Name   | Definition | Pattern |
|--------|------------| ------- |
| Package ID | The unique ID of the package       | [a-z0-9.]
| Author     | The author of the package          | [A-Za-z0-9]
| Name       | The name of the package            | [A-Za-z0-9]
| Latest     | The latest version of the package  | version ref

#### 4.1.1 Package ID
The package ID is a unique identifier for the package, and is derived form the ``Author`` and the ``Name`` metadata. The id is constructed as follows ``<Author>.<Name>`` for example: ``curle.erythro``.
While the ``Author`` and ``Name`` fields can contain upper and lower case letters the package ID must always use their lover case variants.

#### 4.1.2 Author
This denotes the author of the package and can use alpha numeric character, but can't contain ``.``.

#### 4.1.3 Name
This denotes the name of the package and can use alpha numeric character, but can't contain ``.``.
The usage of ``.`` is restricted for future possibility of sub packages or indicating package relations.

#### 4.1.4 Latest
This denotes the version that should be downloaded by default. How this data is stored is left as an implementation detail.


## 4.2 Package Versions
The package versions are what hold the actual payloads of the package for each version. They are identified by a Semver id.

### 4.2.1 Version payload
The version payload can be any file that represents that programs at that version. It will be downloaded to the clients specified folder.

In future versions this is expected to be expanded for multiple files.

# 5 The API
The POK API is using standard http/https as the transport protocol on the port 15060 witch is currently unassigned according to [iana](https://www.iana.org/assignments/service-names-port-numbers/service-names-port-numbers.txt). 
Only the server must expose http endpoints for the clients to call.
Both the server and client must be able to accomodate future port changes as the port is not registerd with IANA as of the writing of this document.

## 5.1 The Server
The server is responsible for storing a database of all the packages and versions, as well as the payloads for the versions.
The server must expose the following api:

> In all the following examples ``<ADDRESS>`` notation is used for parts of a http request that can change.

### 5.1.2 Search
The search API is composed of the following endpoint:

#### GET ``http://<ADDRESS>:<PORT>/search/<SEARCH_TERM>``
The server must search it1s database and try to find the best match for the ``SEARCH_TERM`` and return the following data as ``application/json``
```json
{
    "name": "<NAME>",
    "author": "<AUTHOR>",
    "versions": [
      "a", "b", "c", "d", "e", "f", "g"
    ]
}
```
Where the ``<NAME>`` and ``<AUTHOR>`` represent the name and author metadata of the package.
The version array contains the available versions and the last item in the list denotes the version corresponding to the ``latest`` metadata.