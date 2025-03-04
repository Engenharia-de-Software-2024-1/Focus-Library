package com.apprestriction

import android.content.pm.PackageManager
import android.content.Context
import android.content.pm.ApplicationInfo

class AppRestriction(private val context: Context) {
    fun getInstalledApps(): List<ApplicationInfo> {
        return context.packageManager.getInstalledApplications(PackageManager.GET_META_DATA)
    }
}