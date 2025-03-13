package com.supressnotifications

import android.content.Context
import android.app.NotificationManager
import android.util.Log

object SupressNotifications {

    fun supressAllNotifications(context: Context) {
        val notificationManager = context.getSystemService(Context.NOTIFICATION_SERVICE) as NotificationManager
        if (notificationManager.isNotificationPolicyAccessGranted) {
            notificationManager.setInterruptionFilter(NotificationManager.INTERRUPTION_FILTER_NONE)
        } else {
            Log.d("SupressNotifications", "Notification policy access not granted")
        }
    }
}