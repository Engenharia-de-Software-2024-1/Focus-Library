package com.supressnotifications

import android.content.Context
import android.content.Intent
import android.app.NotificationManager
import android.util.Log
import android.provider.Settings

object SupressNotifications {

    @JvmStatic
    fun supressAllNotifications(context: Context) {
        val notificationManager = context.getSystemService(Context.NOTIFICATION_SERVICE) as NotificationManager
        if (notificationManager.isNotificationPolicyAccessGranted) {
            notificationManager.setInterruptionFilter(NotificationManager.INTERRUPTION_FILTER_NONE)
        } else {
            Log.d("SupressNotifications", "Notification policy access not granted")
        }
    }

    @JvmStatic
    fun startAllNotifications(context: Context) {
        val notificationManager = context.getSystemService(Context.NOTIFICATION_SERVICE) as NotificationManager
        if (notificationManager.isNotificationPolicyAccessGranted) {
            notificationManager.setInterruptionFilter(NotificationManager.INTERRUPTION_FILTER_ALL)
        } else {
            Log.d("StartNotifications", "Notification policy access not granted")
        }
    }

    @JvmStatic
    fun askForNotificationPolicyAccess(context: Context) {
        val intent = Intent(Settings.ACTION_NOTIFICATION_POLICY_ACCESS_SETTINGS)
        intent.putExtra(Settings.EXTRA_APP_PACKAGE, context.getPackageName())
        context.startActivity(intent)
    }

    @JvmStatic
    fun isNotificationPolicyAccessGranted(context: Context): Boolean {
        val notificationManager = context.getSystemService(Context.NOTIFICATION_SERVICE) as NotificationManager
        return notificationManager.isNotificationPolicyAccessGranted
    }
}