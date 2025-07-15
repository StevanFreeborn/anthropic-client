# Change Log

All notable changes to this project will be documented in this file. See [versionize](https://github.com/versionize/versionize) for commit guidelines.

<a name="1.1.0"></a>
## [1.1.0](https://www.github.com/StevanFreeborn/anthropic-client/releases/tag/v1.1.0) (2025-07-15)

### Features

* add missing model constants ([b859859](https://www.github.com/StevanFreeborn/anthropic-client/commit/b8598595484739027bdd83cd8cdfe122ad3cb9cf))
* add support for file source and url source ([1f83899](https://www.github.com/StevanFreeborn/anthropic-client/commit/1f83899eb88b9ee9d87d246158defd7cbde9b01e))
* implement `GetFileInfoAsync`, `GetFileAsync`, and `DeleteFileAsync` ([d1e88a5](https://www.github.com/StevanFreeborn/anthropic-client/commit/d1e88a52ace603a5c017cca3bf48953846d7b271))
* implement list all files method ([9cb8443](https://www.github.com/StevanFreeborn/anthropic-client/commit/9cb8443f94d2086192700aeb1f2dc977a86cb752))
* implement listing a page of files ([9d5c620](https://www.github.com/StevanFreeborn/anthropic-client/commit/9d5c6201674bfb0923a0b0d70530286980ebdf62))
* initial implementation of creating a file via the Files API ([e674b9a](https://www.github.com/StevanFreeborn/anthropic-client/commit/e674b9afe693143841a168c8356ef93079445db0))

### Bug Fixes

* remove unnecessary usings ([1cad19d](https://www.github.com/StevanFreeborn/anthropic-client/commit/1cad19d9c6c4418fef3c206f700ca14b7d250e26))
* use sync copy to method ([b48baff](https://www.github.com/StevanFreeborn/anthropic-client/commit/b48baffb60a8acc8b89a60e53589bc04362a9505))

<a name="1.0.0"></a>
## [1.0.0](https://www.github.com/StevanFreeborn/anthropic-client/releases/tag/v1.0.0) (2025-07-09)

### Features

* add serialization tests for CharacterLocationCitation, CitationDelta, ContentBlockLocationCitation, PageLocationCitation, and DocumentContent classes ([598eaaa](https://www.github.com/StevanFreeborn/anthropic-client/commit/598eaaad9412c5bbbe9e28d2d87fcb0b38592fd6))
* add support for citations ([5b8d88a](https://www.github.com/StevanFreeborn/anthropic-client/commit/5b8d88a01d2f1933ddf60bfffa908e8e220bef8c))
* add support for citations when streaming ([98da138](https://www.github.com/StevanFreeborn/anthropic-client/commit/98da1384c154e0300e44d41cd160c15e522e7dbd))
* add unit tests for DocumentContent, CustomSource, PageLocationCitation, SourceType, and TextSource classes ([68e91fc](https://www.github.com/StevanFreeborn/anthropic-client/commit/68e91fcdbb032fe333744f4d2f5e696891f0c240))
* initial pass at citations in request and response ([a9d0dfd](https://www.github.com/StevanFreeborn/anthropic-client/commit/a9d0dfdc7607ff6d6e60afbfbe7c14973ff113a8))

### Bug Fixes

* address copilot comments ([e351823](https://www.github.com/StevanFreeborn/anthropic-client/commit/e351823f37828f93305884d60a5d6601db2eba08))
* when serializing source use media type to deserialize to image or document source ([74ac1ff](https://www.github.com/StevanFreeborn/anthropic-client/commit/74ac1ffc06327e63571cf89050759747776bce25))

### Breaking Changes

* add support for citations ([5b8d88a](https://www.github.com/StevanFreeborn/anthropic-client/commit/5b8d88a01d2f1933ddf60bfffa908e8e220bef8c))

<a name="0.6.1"></a>
## [0.6.1](https://www.github.com/StevanFreeborn/anthropic-client/releases/tag/v0.6.1) (2025-05-19)

### Bug Fixes

* cleanup based on copilot review comments ([5d4fb29](https://www.github.com/StevanFreeborn/anthropic-client/commit/5d4fb290ba24095ea564cf8a33366dd53ab4ba44))
* cleanup copilot mistakes with passing cancellation token to methods that don't accept them ([595326c](https://www.github.com/StevanFreeborn/anthropic-client/commit/595326c7a9454e3c9d4a5c9d95777e723896e1a0))
* copilot first attempt ([0a1c6a0](https://www.github.com/StevanFreeborn/anthropic-client/commit/0a1c6a0135d997baa5475d1f6417772bae160a05))

<a name="0.6.0"></a>
## [0.6.0](https://www.github.com/StevanFreeborn/anthropic-client/releases/tag/v0.6.0) (2025-01-15)

### Features

* add CancelMessageBatchAsync method and corresponding tests ([c11600c](https://www.github.com/StevanFreeborn/anthropic-client/commit/c11600c77040eebfab4df1153d07774357d87ce2))
* add class for representing batch statuses ([f8e01bf](https://www.github.com/StevanFreeborn/anthropic-client/commit/f8e01bf4878e07f823d524408ed59a2232960a7b))
* add DeleteMessageBatchAsync method and MessageBatchDeleteResponse model with tests ([56b3a53](https://www.github.com/StevanFreeborn/anthropic-client/commit/56b3a53a2a31070693351525379b6a675b068496))
* add ListAllMessageBatchesAsync method and corresponding tests ([180a090](https://www.github.com/StevanFreeborn/anthropic-client/commit/180a0901959b88b27981f063b0be7a267a141802))
* implement CreateMessageBatchAsync method ([9548754](https://www.github.com/StevanFreeborn/anthropic-client/commit/9548754d6cab73d21c7b7a28264374e79dcb8bab))
* implement GetMessageBatchAsync method ([c5f3c5e](https://www.github.com/StevanFreeborn/anthropic-client/commit/c5f3c5eca549a89ac6b8197b833767c0ca4465c5))
* implement GetMessageBatchResultsAsync method ([c56adf3](https://www.github.com/StevanFreeborn/anthropic-client/commit/c56adf394c62aca10c89f1d8650635cefe248d50))
* implement ListMessageBatchesAsync method ([ca2ecdc](https://www.github.com/StevanFreeborn/anthropic-client/commit/ca2ecdcfc79a5ff96c22eb564e7fc8be22e82a6f))

### Bug Fixes

* default nullable properties to actually datetimeoffset min value when new'd up ([0a6d89b](https://www.github.com/StevanFreeborn/anthropic-client/commit/0a6d89bd77e165a0ae63dacd9f854bd49ba817ce))
* make correct properties nullable ([15f5ad4](https://www.github.com/StevanFreeborn/anthropic-client/commit/15f5ad49e7f41215c3b089cdfe3873182223ce2d))
* use proper count tokens endpoint ([5e247a3](https://www.github.com/StevanFreeborn/anthropic-client/commit/5e247a3c3afb9a3dad954eaa5890de106ae2edb9))

<a name="0.5.0"></a>
## [0.5.0](https://www.github.com/StevanFreeborn/anthropic-client/releases/tag/v0.5.0) (2025-01-06)

### Features

* add classes to model response to get model list endpoint ([28b7bf6](https://www.github.com/StevanFreeborn/anthropic-client/commit/28b7bf6a897cf3b2e2aad7df767b0f4428bec7a4))
* add model to represent paged request ([df75cd2](https://www.github.com/StevanFreeborn/anthropic-client/commit/df75cd25e0a1beb55be97d5fcb8a718806793f51))
* implement GetModelAsync method ([8bbd4bb](https://www.github.com/StevanFreeborn/anthropic-client/commit/8bbd4bb7c8c3f4e6fdd6d85d68d728794b85831f))
* implement ListAllModelsAsync and ListModelsAsync ([8e0443a](https://www.github.com/StevanFreeborn/anthropic-client/commit/8e0443a6bb4736207d5ddf173f8ec966d03cec46))

### Bug Fixes

* only allow afterId or beforeId to be set. not both at same time. ([3fe75f5](https://www.github.com/StevanFreeborn/anthropic-client/commit/3fe75f511d6c6adae7971b6402353c7408b6d513))

<a name="0.4.0"></a>
## [0.4.0](https://www.github.com/StevanFreeborn/anthropic-client/releases/tag/v0.4.0) (2025-01-03)

### Features

* add support for count tokens endpoint ([9448220](https://www.github.com/StevanFreeborn/anthropic-client/commit/944822060cd297c5720d8a1dc366f08ca0ec79ec))

### Bug Fixes

* remove model id validation ([31a7097](https://www.github.com/StevanFreeborn/anthropic-client/commit/31a7097cdc9f9f60a7b6576b8bcee7a49a00552c))

<a name="0.3.0"></a>
## [0.3.0](https://www.github.com/StevanFreeborn/anthropic-client/releases/tag/v0.3.0) (2025-01-02)

### Features

* add missing model identifiers ([6e0c065](https://www.github.com/StevanFreeborn/anthropic-client/commit/6e0c0654af6a7daebac541995e4f082bae4a052e))

<a name="0.2.0"></a>
## [0.2.0](https://www.github.com/StevanFreeborn/anthropic-client/releases/tag/v0.2.0) (2024-11-21)

### Features

* add content type ([c4d4d8a](https://www.github.com/StevanFreeborn/anthropic-client/commit/c4d4d8a15bb690ae2b766a8f797b9e49f6965e60))
* add model for DocumentContent and DocumentSource ([f3a7040](https://www.github.com/StevanFreeborn/anthropic-client/commit/f3a7040cb929ac55df2b8369a26bb1b1a8e29fc8))

<a name="0.1.1"></a>
## [0.1.1](https://www.github.com/StevanFreeborn/anthropic-client/releases/tag/v0.1.1) (2024-11-19)

### Bug Fixes

* address vulnerabilities in dependencies ([24bd877](https://www.github.com/StevanFreeborn/anthropic-client/commit/24bd877c095c7e65fd1d90ad36cc263800dc54ea))

<a name="0.1.0"></a>
## [0.1.0](https://www.github.com/StevanFreeborn/anthropic-client/releases/tag/v0.1.0) (2024-08-18)

### Features

* add constructor to allow setting cache control on tool result content ([a3c7e90](https://www.github.com/StevanFreeborn/anthropic-client/commit/a3c7e9073465a0db3a69927f0b8361e9b670266d))
* add type for ephemeral cache control ([35adac9](https://www.github.com/StevanFreeborn/anthropic-client/commit/35adac9fa0f3156c07e1fc4324b8fb2c1b50acd8))
* first take of adding caching support ([ed97ea9](https://www.github.com/StevanFreeborn/anthropic-client/commit/ed97ea95dc4a2f0bfb9fadd284aad6fed1d4b7e1))

### Bug Fixes

* give cache control proper json property name ([cc1d628](https://www.github.com/StevanFreeborn/anthropic-client/commit/cc1d628feb482655c0b2b1b4675f98fc47f5f584))
* make cache control class abstract ([6431e2a](https://www.github.com/StevanFreeborn/anthropic-client/commit/6431e2a113b9a87a53c4a1ddd2c1ef49f5e3e50d))
* make cache control setter public ([d458c9e](https://www.github.com/StevanFreeborn/anthropic-client/commit/d458c9ed23a4f575304d0c16db591c1081d6d118))
* make sure to pass cache control to tool constructor ([539d735](https://www.github.com/StevanFreeborn/anthropic-client/commit/539d735e6b244715f1e30c67175bf8f2eb6f5729))
* remove converter ([6600871](https://www.github.com/StevanFreeborn/anthropic-client/commit/66008717e1392f6667e2acc0d5016059adc2e491))
* reuse validation logic in constructors ([987fbe9](https://www.github.com/StevanFreeborn/anthropic-client/commit/987fbe974236379f788df4fca2f0326901b2c094))
* use additional constructor param with default value instead of overloaded constructor to avoid potentially breaking others code with that would then contain ambigious constructor calls. ([90e9b9f](https://www.github.com/StevanFreeborn/anthropic-client/commit/90e9b9f75eec1488277538fff5f90b817ded3b6b))

### Other

* Merge pull request #12 from StevanFreeborn/stevanfreeborn/tests/fix-image-test [skip ci] ([c0dec5f](https://www.github.com/StevanFreeborn/anthropic-client/commit/c0dec5f7de35977af52db86e1977cf8529e2f134))
* Merge pull request #14 from StevanFreeborn/stevanfreeborn/feat/add-support-for-prompt-caching ([dc162aa](https://www.github.com/StevanFreeborn/anthropic-client/commit/dc162aa80beb3d2a7792b04311933002da82dcaf))
* add method for creating client with customized http client ([eb0befe](https://www.github.com/StevanFreeborn/anthropic-client/commit/eb0befe62a1585c7fe26e0e1ff616e27b6dd52ff))
* add test for cache control type static class ([364c910](https://www.github.com/StevanFreeborn/anthropic-client/commit/364c91080c463fcb2249dfee37745c8ff20c7586))
* add test for serializing system property with correct expected value based on whether given system messages or just a system message. ([f6460e9](https://www.github.com/StevanFreeborn/anthropic-client/commit/f6460e99ee5195342b63b71e4e1a720eaaf4432e))
* add test to make sure cache control can be set on tool use content objects ([e47095e](https://www.github.com/StevanFreeborn/anthropic-client/commit/e47095e03dc49d5a3954f37cc928b3eaced04779))
* add tests for constructor using cache control ([0c160e9](https://www.github.com/StevanFreeborn/anthropic-client/commit/0c160e998a628af0f21883e592ddc8fb902f170f))
* add tests for creating tools with cache control set ([42d6ee4](https://www.github.com/StevanFreeborn/anthropic-client/commit/42d6ee4d3443f7d8a3962a07fd5f7fc6b919e5ff))
* add tests for ephemeral cache control model ([f2f1507](https://www.github.com/StevanFreeborn/anthropic-client/commit/f2f150791ec5ba4bced53094bafbc95e8f9ae087))
* add tests for overloaded constructor ([0253794](https://www.github.com/StevanFreeborn/anthropic-client/commit/025379423527677d9fab77ca92a5ca4004ea1571))
* add text greater than 2048 tokens for testing caching ([2e1c111](https://www.github.com/StevanFreeborn/anthropic-client/commit/2e1c1118c36878cedfde8fee7a645eadfa1ed418))
* added end to end tests for cache control when caching system messages, user messages, or tools ([4524d7b](https://www.github.com/StevanFreeborn/anthropic-client/commit/4524d7be8880a3014656ccb0b6b91ec9b76c0c89))
* documentation for v0.0.4 [skip ci] ([03ad94d](https://www.github.com/StevanFreeborn/anthropic-client/commit/03ad94d9f96035faae01c0ccba92bac985c1e832))
* put request json in intermediate variable to make debugging better ([e04599b](https://www.github.com/StevanFreeborn/anthropic-client/commit/e04599bad9c930d8101ace78ee9c1d234f93e52e))
* remove unused using statement ([07457c1](https://www.github.com/StevanFreeborn/anthropic-client/commit/07457c1808fc7b07bc28a5904a7dd6cfbc7053df))
* run dotnet format ([afe70a9](https://www.github.com/StevanFreeborn/anthropic-client/commit/afe70a90125ab569e5c5b7dceeba2836807f84b3))
* stop git from messing up image file ([051fa17](https://www.github.com/StevanFreeborn/anthropic-client/commit/051fa178aa7d464cc82db0dbcd919260a3f55368))
* update README.md with documentation about using prompt caching with this library. ([c2fa796](https://www.github.com/StevanFreeborn/anthropic-client/commit/c2fa796bd013d55d42468980fb9d9ac98f025264))
* update tests to account for serialization and deserialization changes with new/modified model properties to support caching ([900a7a3](https://www.github.com/StevanFreeborn/anthropic-client/commit/900a7a354941f6b5a1357cacbafb64cd8220d918))
* whitelist word ([f1c02eb](https://www.github.com/StevanFreeborn/anthropic-client/commit/f1c02ebc8faf4804a464d27c2a5d4c663cb82015))

<a name="0.0.4"></a>
## [0.0.4](https://www.github.com/StevanFreeborn/anthropic-client/releases/tag/v0.0.4) (2024-07-20)

### Bug Fixes

* add end to end test for vision ([b3d4ac3](https://www.github.com/StevanFreeborn/anthropic-client/commit/b3d4ac3faa94086369a02b0a47a19db9441b274e))
* add missing type property to image source model ([0a692d1](https://www.github.com/StevanFreeborn/anthropic-client/commit/0a692d1b9a5f39869aef05892f0801fefe31f50e))
* yield error when initial stream response is not successful ([3b47ed3](https://www.github.com/StevanFreeborn/anthropic-client/commit/3b47ed3fb4e8b30a73a8297e6485985fee7c129a))

### Other

* Merge pull request #11 from StevanFreeborn/stevanfreeborn/fix/missing-type-property-and-response-handling ([d27538b](https://www.github.com/StevanFreeborn/anthropic-client/commit/d27538b65620333ade65e920110a21acd829123f))
* add missing type property ([f970b07](https://www.github.com/StevanFreeborn/anthropic-client/commit/f970b07ed79979b94961c93a192b57fc12a81097))
* debug ci failure ([641eaf3](https://www.github.com/StevanFreeborn/anthropic-client/commit/641eaf30a476cece3cc8bb37f147aa42625992d2))
* debug ci failure ([30c0116](https://www.github.com/StevanFreeborn/anthropic-client/commit/30c01161136949471d661284fa86838ac663ec87))
* debug CI failure ([48b2716](https://www.github.com/StevanFreeborn/anthropic-client/commit/48b2716a61894c471bd2b703254ceb7126354f5e))
* debug CI failure ([9b714df](https://www.github.com/StevanFreeborn/anthropic-client/commit/9b714df12f1d7a6e2e500726ac10a345e4c66054))
* debug test failure in CI ([f39e76c](https://www.github.com/StevanFreeborn/anthropic-client/commit/f39e76cfa64f649e9297bab319ef6ddd58995e0e))
* run format ([93761b8](https://www.github.com/StevanFreeborn/anthropic-client/commit/93761b8e7e0ac67f205abd93f6e9db9465924b02))
* use base64 text file ([a16b64d](https://www.github.com/StevanFreeborn/anthropic-client/commit/a16b64d5a079f65884a8f3a2cf15be4da06f73d3))

<a name="0.0.3"></a>
## [0.0.3](https://www.github.com/StevanFreeborn/anthropic-client/releases/tag/v0.0.3) (2024-07-11)

### Bug Fixes

* address nuget health checks ([989951d](https://www.github.com/StevanFreeborn/anthropic-client/commit/989951db552b2305e2fab9131e26d2fcc2e9283a))

### Other

* Merge pull request #8 from StevanFreeborn/stevanfreeborn/feat/address-nuget-health-checks ([f245d64](https://www.github.com/StevanFreeborn/anthropic-client/commit/f245d6442f040d8dcabd27f013b90373ab25370a))
* documentation for v0.0.2 [skip ci] ([d486bc8](https://www.github.com/StevanFreeborn/anthropic-client/commit/d486bc869101e9ca87dfc60f9aa6b6ad12f073de))
* upgrade System.Text.Json dep ([5576b68](https://www.github.com/StevanFreeborn/anthropic-client/commit/5576b688c50b21031f68fca7e2728d1398a366c5))

<a name="0.0.2"></a>
## [0.0.2](https://www.github.com/StevanFreeborn/anthropic-client/releases/tag/v0.0.2) (2024-07-10)

### Bug Fixes

* add stop sequences parameter ([0363756](https://www.github.com/StevanFreeborn/anthropic-client/commit/0363756571a00d951f185502d86aac4bbdc6e570))

### Other

* Merge branch 'main' of github.com:StevanFreeborn/anthropic-client ([3b9dc67](https://www.github.com/StevanFreeborn/anthropic-client/commit/3b9dc6706e47a3deca861e5306498e91f52b6fb8))
* Merge pull request #5 from StevanFreeborn/stevanfreeborn/fix/allow-stop-sequence-to-be-passed ([3bdb417](https://www.github.com/StevanFreeborn/anthropic-client/commit/3bdb4173f31f04446d04a9b2479d39850895c5fc))
* correct xml comment ([48f813f](https://www.github.com/StevanFreeborn/anthropic-client/commit/48f813f49fbcf13c298017634bd6c8d7638f9edb))
* documentation for v0.0.1 [skip ci] ([0a2ea19](https://www.github.com/StevanFreeborn/anthropic-client/commit/0a2ea1934d936b8b613388a4014cd1286e798825))
* update gitattributes to treat font files as binary ([2726426](https://www.github.com/StevanFreeborn/anthropic-client/commit/27264261ce0c71e241d2de82ffe3ee076159dca9))
* update workflow to continue on error ([7c04f0a](https://www.github.com/StevanFreeborn/anthropic-client/commit/7c04f0a29c38903ebcb535db5619d9f40387aacb))

<a name="0.0.1"></a>
## [0.0.1](https://www.github.com/StevanFreeborn/anthropic-client/releases/tag/v0.0.1) (2024-07-07)

### Bug Fixes

* add correct package project url ([afee3b2](https://www.github.com/StevanFreeborn/anthropic-client/commit/afee3b293a69ed616dedb7656a05cda74502f7a9))

### Other

* Create CNAME ([2e64dcb](https://www.github.com/StevanFreeborn/anthropic-client/commit/2e64dcb7dbbc11180b8c6f56c50a77e2f6bdc13a))
* Merge branch 'main' of github.com:StevanFreeborn/anthropic-client ([4f156a5](https://www.github.com/StevanFreeborn/anthropic-client/commit/4f156a53cf467ee809136e1d8c66167bbb6890be))
* Merge branch 'main' of github.com:StevanFreeborn/anthropic-client ([56f74c6](https://www.github.com/StevanFreeborn/anthropic-client/commit/56f74c68c9ccf2e2ef4222e1bcfe0149f0db1046))
* Merge branch 'main' of github.com:StevanFreeborn/anthropic-client ([78d75eb](https://www.github.com/StevanFreeborn/anthropic-client/commit/78d75eb9440d07a2f121105be6d3382249542c1d))
* Merge branch 'main' of github.com:StevanFreeborn/anthropic-client ([ddeb651](https://www.github.com/StevanFreeborn/anthropic-client/commit/ddeb6519a2e314ac9194a624e91d4953a1fd3c12))
* add badges ([599a11c](https://www.github.com/StevanFreeborn/anthropic-client/commit/599a11cd2ded815cc2dceab49aa6140193a4e4ab))
* add material theme for documentation site ([8a6af15](https://www.github.com/StevanFreeborn/anthropic-client/commit/8a6af152b05f4deb220501de4c69adbde98f7bb2))
* documentation for v0.0.0 [skip ci] ([7ad471d](https://www.github.com/StevanFreeborn/anthropic-client/commit/7ad471d7608dc89c1efa9803c055123d7929f2bf))
* documentation for v0.0.0 [skip ci] ([57e950e](https://www.github.com/StevanFreeborn/anthropic-client/commit/57e950e1ce360d416bd98f204e5b17f28c51a8a1))
* documentation for v0.0.0 [skip ci] ([2879bfd](https://www.github.com/StevanFreeborn/anthropic-client/commit/2879bfde33f0c26df5e79346b8ca594c40585e8a))
* documentation for v0.0.0 [skip ci] ([9d6935c](https://www.github.com/StevanFreeborn/anthropic-client/commit/9d6935c9eb7d9518ab797be5fa4cff7dc852e565))
* fix workflow ([1de786c](https://www.github.com/StevanFreeborn/anthropic-client/commit/1de786c27c709574104f5563e439ff10fe7902c9))
* remove PDF option ([6eaaaef](https://www.github.com/StevanFreeborn/anthropic-client/commit/6eaaaef3b624dca4b674dbabb6f17e5fe3f0e95e))
* workout documentation job ([2a7dd11](https://www.github.com/StevanFreeborn/anthropic-client/commit/2a7dd118c6dadf2b5675282111f76cfb5bd5566f))

<a name="0.0.0"></a>
## [0.0.0](https://www.github.com/StevanFreeborn/anthropic-client/releases/tag/v0.0.0) (2024-07-07)

### Features

* add class to represent a callable function ([691ffaa](https://www.github.com/StevanFreeborn/anthropic-client/commit/691ffaafb735c670d3c49b382493283cff5b8568))
* add function property attribute ([f848d4b](https://www.github.com/StevanFreeborn/anthropic-client/commit/f848d4b79235ac11b204375dac6afca7247b7ddb))
* add method to throw if null or whitespace ([3298db7](https://www.github.com/StevanFreeborn/anthropic-client/commit/3298db7a3add483eaeda1fcd8e72b8c12f72d34f))
* add models for streaming messages ([a4784fd](https://www.github.com/StevanFreeborn/anthropic-client/commit/a4784fd5dbdeb299f61d1ff58ebf54287da48630))
* add parameter attribute to allow customization of name, description, and requiredness ([3006ad9](https://www.github.com/StevanFreeborn/anthropic-client/commit/3006ad9b50f90a1e1938293518fd6f9620725a3d))
* add support for delegate with two parameters ([4dd9d16](https://www.github.com/StevanFreeborn/anthropic-client/commit/4dd9d168f5c75409c8a12d8c4d4d63875f5e91ee))
* begin work on creating input schema from function method info ([087f012](https://www.github.com/StevanFreeborn/anthropic-client/commit/087f0120931f7276aa005953f36ede10b2c63cdc))
* create chat message ([4c2cee6](https://www.github.com/StevanFreeborn/anthropic-client/commit/4c2cee643a0d7d88a25a7097a0f0f333b0d693f6))
* create tool from class that implements ITool ([e3ba6a6](https://www.github.com/StevanFreeborn/anthropic-client/commit/e3ba6a62874079519751c1a4b6f7d9d2397165a2))
* generate definition for complex types ([057803a](https://www.github.com/StevanFreeborn/anthropic-client/commit/057803a8f90ea724fd1084189943a2f7ad0ca4fb))
* implement json schema generation for non-complex types ([96ed89c](https://www.github.com/StevanFreeborn/anthropic-client/commit/96ed89c9ceb7cb0ca9b9985c84c64a40d4c10b60))
* implement tool call ([ce6b1bf](https://www.github.com/StevanFreeborn/anthropic-client/commit/ce6b1bf8dc776825f450f17d89b660a81d3c31ae))
* yield events for streaming including custom message complete event ([6ed99a6](https://www.github.com/StevanFreeborn/anthropic-client/commit/6ed99a6a23b9c5b27cef5b4e9f22e28a2fc0e82f))

### Bug Fixes

* add string enum converter to serialization options ([9c81620](https://www.github.com/StevanFreeborn/anthropic-client/commit/9c8162036ed3e95b400911fd9071df06ac4ca3d4))
* address methods that return void or task in those cases return null ([c2788c3](https://www.github.com/StevanFreeborn/anthropic-client/commit/c2788c389320c6b89e4d9109273fe082ea4b519c))
* cleanup unnecessary usings ([0562c0c](https://www.github.com/StevanFreeborn/anthropic-client/commit/0562c0c1aa8b841933eea36f7dcbf8ec613e6a9c))
* correct nullability ([fec7de2](https://www.github.com/StevanFreeborn/anthropic-client/commit/fec7de2f663e5efe486a40770d3ded4e3450e678))
* correct validation for create methods and update xml comments to match ([94434c7](https://www.github.com/StevanFreeborn/anthropic-client/commit/94434c7092a8879b4d9f705a368d899ba8022f85))
* deal with nested complex types ([b594927](https://www.github.com/StevanFreeborn/anthropic-client/commit/b594927df072d1bbe3169abfe925e05e7d5755f9))
* enforce anthropic's name limitations ([a5af463](https://www.github.com/StevanFreeborn/anthropic-client/commit/a5af4636b9c8a4ea61c7a01797a3a99c1c480282))
* make sure custom parameter name is not whitespace ([cccd0e6](https://www.github.com/StevanFreeborn/anthropic-client/commit/cccd0e6647099ae328a762e168a7c0fcc0eeff61))
* modify tool class to have static methods and create input schema from function ([0515afb](https://www.github.com/StevanFreeborn/anthropic-client/commit/0515afb1e004e8c798968c4bfd591b564f84c42e))
* move client to top level namespace ([422719f](https://www.github.com/StevanFreeborn/anthropic-client/commit/422719f509fe456cd808cad78473f3b76e9a94d2))
* remove redundant validation and add xml comments ([606c81d](https://www.github.com/StevanFreeborn/anthropic-client/commit/606c81d38484a6b75356fa60e280f0f8b1878926))
* remove throw if name is invalid ([da93ffd](https://www.github.com/StevanFreeborn/anthropic-client/commit/da93ffd1f02861b8a1154e6354b64b6c220787ae))
* remove unnecessary constant ([77ceb49](https://www.github.com/StevanFreeborn/anthropic-client/commit/77ceb49c775f426199e3eede8e8b0c5ab562a9f7))
* remove unused member and move comment ([15315df](https://www.github.com/StevanFreeborn/anthropic-client/commit/15315df3c7b187fc8ccb78745af31bb74dbcda4b))
* throw exception when accessing result props incorrectly ([c538a3e](https://www.github.com/StevanFreeborn/anthropic-client/commit/c538a3eb8c5de6406534ce3d457a0ef20e20cddc))
* use json object type ([4fd8ecd](https://www.github.com/StevanFreeborn/anthropic-client/commit/4fd8ecd96b6ff6000a52ae24d90a19e6689704d2))
* use name of member not member type ([9eb0238](https://www.github.com/StevanFreeborn/anthropic-client/commit/9eb023887c17693b9c9e3434976d6a4d28ab0031))
* use trygetvalues ([ffee24d](https://www.github.com/StevanFreeborn/anthropic-client/commit/ffee24d8ecd6008637456dacca086b08e9c3cf0b))

### Other

* Merge branch 'main' of github.com:StevanFreeborn/anthropic-client ([98e9596](https://www.github.com/StevanFreeborn/anthropic-client/commit/98e9596070422bc6488b249a3f752de7942496ff))
* Merge pull request #1 from StevanFreeborn/stevanfreeborn/docs/prepare-documentation-for-release ([3969937](https://www.github.com/StevanFreeborn/anthropic-client/commit/3969937ffadc19379f848771cdee21b1257efb0d))
* Merge pull request #2 from StevanFreeborn/stevanfreeborn/chore/add-workflows ([1e7fa66](https://www.github.com/StevanFreeborn/anthropic-client/commit/1e7fa6686510814d7da95719491555f9dd95a369))
* add additional streaming tests ([bd8e4c7](https://www.github.com/StevanFreeborn/anthropic-client/commit/bd8e4c70ab2398ba3ae2d032019b592added4df9))
* add additional test cases ([0cb9918](https://www.github.com/StevanFreeborn/anthropic-client/commit/0cb9918a83b6b21df65de79cca58c44864a0a79c))
* add dispatch event to workflows ([b6baef7](https://www.github.com/StevanFreeborn/anthropic-client/commit/b6baef7f50dc53675d14c17746291232a0d57956))
* add git attributes file ([cc6f682](https://www.github.com/StevanFreeborn/anthropic-client/commit/cc6f6820ecebf15c62609f21877cb40ae6154891))
* add missing test cases ([aed18b3](https://www.github.com/StevanFreeborn/anthropic-client/commit/aed18b35fa9cf8d94bae7b3a510fb9184dcc1f15))
* add missing tests for streaming chat message ([5edc0b5](https://www.github.com/StevanFreeborn/anthropic-client/commit/5edc0b5fff0e040d58e2a9e40b5482f88b0a2d3f))
* add missing type params to expected schema ([5fa85b7](https://www.github.com/StevanFreeborn/anthropic-client/commit/5fa85b7e8bbf2e50a68d43341f64fadf12ae8b49))
* add missing xml comments ([1662479](https://www.github.com/StevanFreeborn/anthropic-client/commit/16624798bf6e558d41bb6b1593d2eb5bb727692c))
* add more tests for tool class ([ecf1958](https://www.github.com/StevanFreeborn/anthropic-client/commit/ecf1958fd3d1864af3239e719d80fa903fce6570))
* add package metadat ([d1855dd](https://www.github.com/StevanFreeborn/anthropic-client/commit/d1855ddd3706c091f6f66e88f9bfd95769d04e20))
* add test cases ([ff649cb](https://www.github.com/StevanFreeborn/anthropic-client/commit/ff649cb3d19afa8c8dd4b539f06014461ff7c187))
* add workflows ([ebbae05](https://www.github.com/StevanFreeborn/anthropic-client/commit/ebbae05a22472cf3976c0e7c072d5417b6c8b5e5))
* delete change log ([6067d5d](https://www.github.com/StevanFreeborn/anthropic-client/commit/6067d5d70c2a6fb80f142fb9325d0765d4af0564))
* don't reuse http client as fixture ([6caa4b1](https://www.github.com/StevanFreeborn/anthropic-client/commit/6caa4b1b069bf773d034d5f2aa7fa68f07a6dfb9))
* fix branch name ([be836d6](https://www.github.com/StevanFreeborn/anthropic-client/commit/be836d6f485543ccc6174966e262e3d838d4451d))
* fix dotnet format command ([d5cfe65](https://www.github.com/StevanFreeborn/anthropic-client/commit/d5cfe65622030a6cd02fef2f86660847d09cdcfe))
* initial project setup ([68b3591](https://www.github.com/StevanFreeborn/anthropic-client/commit/68b359163d7bb3d46db98a22dd25030f7466ba39))
* make sure there is content in message returned ([fdcd4a0](https://www.github.com/StevanFreeborn/anthropic-client/commit/fdcd4a05f286a3f9260c7db2f5425de8107b3bd7))
* make test settings optional ([c2e21ac](https://www.github.com/StevanFreeborn/anthropic-client/commit/c2e21ac574ba1ec4e128d7b7d4e7321a20e6973d))
* more work on workflows ([554c696](https://www.github.com/StevanFreeborn/anthropic-client/commit/554c696069d210fe1ae7085e9383ba04107869fb))
* run dotnet format ([3851bf4](https://www.github.com/StevanFreeborn/anthropic-client/commit/3851bf41c02170e4de7022789ea7ca6dbacac3c0))
* run dotnet format ([1ae9010](https://www.github.com/StevanFreeborn/anthropic-client/commit/1ae90106b5ed6c06b4495fe2c3440922577aa0a4))
* run format ([f85771c](https://www.github.com/StevanFreeborn/anthropic-client/commit/f85771c664961f4e8a4257bebd48cd37c5387031))
* update README.md ([78497b8](https://www.github.com/StevanFreeborn/anthropic-client/commit/78497b823572127b9802d80dff4fc7ca92ca169e))
* update README.md ([22b5f6b](https://www.github.com/StevanFreeborn/anthropic-client/commit/22b5f6bda26cfab3c64476fbd0b5062df214cca0))
* update README.md ([8c0c60f](https://www.github.com/StevanFreeborn/anthropic-client/commit/8c0c60faeaa813e6669ef065812d699ddff7fd5a))
* update test case name ([04343bf](https://www.github.com/StevanFreeborn/anthropic-client/commit/04343bf732e079b7a2896e94c38836e29df6455c))
* update tests to allow building while reworking tool feature ([4bf99d6](https://www.github.com/StevanFreeborn/anthropic-client/commit/4bf99d69ddcf64ca6f20eb8379bdf8c86008329f))
* use docfx to generate documentation ([3e69208](https://www.github.com/StevanFreeborn/anthropic-client/commit/3e6920868125d22310717f4166d1d9e1c582a6cc))
* work on adding integration tests ([917719d](https://www.github.com/StevanFreeborn/anthropic-client/commit/917719d36e6ff9021d411774289365ac00fb8f57))
* work on adding integration tests ([ebae270](https://www.github.com/StevanFreeborn/anthropic-client/commit/ebae27077ebaf4b1bd867560b589d62fe0119449))
* work on README ([1d352a6](https://www.github.com/StevanFreeborn/anthropic-client/commit/1d352a6f95faca8d6f5eff7901dfff57007ad030))
* work on README ([5c0cad2](https://www.github.com/StevanFreeborn/anthropic-client/commit/5c0cad254583781e53de81ba8fec16eafa408b72))
* work on README ([dd380c9](https://www.github.com/StevanFreeborn/anthropic-client/commit/dd380c9253d2b00a2823ccc49c5a67bd47c88227))
* workflow fix mispelling ([dde8543](https://www.github.com/StevanFreeborn/anthropic-client/commit/dde8543b627487a302d075391abe3f0c77583d87))
* **release:** 0.0.0 [skip ci] ([f2b3e0e](https://www.github.com/StevanFreeborn/anthropic-client/commit/f2b3e0e7d32a3e0a0de11ef3c2cb43da1283255a))

