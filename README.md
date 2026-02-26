# Tealium Sitecore Module

> **⚠️ SECURITY WARNING**
>
> **This module contains a critical XSS (Cross-Site Scripting) vulnerability** that allows attackers to inject arbitrary JavaScript through user-controlled input (e.g., URL parameters). Exploitation can lead to session hijacking, credential theft, and other attacks.
>
> **See GitHub Issue [#4](https://github.com/Tealium/integration-sitecore/issues/4) for technical details.**

> **🚫 DEPRECATED - February 2026**
>
> **This module is officially deprecated and will receive no further updates after version 1.0.2.4.**
>
> This final release (v1.0.2.4) addresses the critical XSS vulnerability and updates the Newtonsoft.Json dependency to resolve a known DoS vulnerability. However, **no further security patches or feature updates will be provided**.
>
> **Migration Recommended:**
> - **Option 1:** Use modern Tealium tracking via standard JavaScript (iQ Tag Management) with proper server-side encoding of all dynamic values before rendering into HTML/JavaScript contexts.
> - **Option 2:** Migrate to server-side EventStream connectors that eliminate client-side injection risks entirely.
>
> For assistance with migration, please contact Tealium Support or your Customer Success Manager.

---

## About

This GitHub repository contains Tealium's Sitecore integration module and related files.  For importing directly into Sitecore, please download the latest package .zip file found in the "releases" section of this repository.

## License

Use of this software is subject to the terms and conditions of the license agreement contained in the file titled "LICENSE.txt".  Please read the license before downloading or using any of the files contained in this repository. By downloading or using any of these files, you are agreeing to be bound by and comply with the license agreement.

## Trademarks

Sitecore is a registered trademarks of Sitecore Corporation A/S in the U.S. and other countries . All rights reserved. Tealium is not an affiliate of Sitecore.

---
Copyright (C) 2012-2017, Tealium Inc.
