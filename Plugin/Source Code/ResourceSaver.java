package ennotoupper.resourcesaver;

import org.bukkit.Server;
import org.bukkit.entity.Player;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.player.PlayerJoinEvent;
import org.bukkit.event.player.PlayerQuitEvent;
import org.bukkit.plugin.java.JavaPlugin;
import org.bukkit.scheduler.BukkitScheduler;
import org.bukkit.scheduler.BukkitTask;

public final class ResourceSaver extends JavaPlugin implements Listener {

    BukkitScheduler bukkitSchedulerCheckEmpty;

    BukkitTask shutdownTask;
    BukkitTask checkPlayerTask;

    Server server;

    long delayCheck = 5L;
    long startDelayCheck = 120L;
    long delayShutdown = 60L;

    @Override
    public void onEnable() {
        // Plugin startup logic
        server = getServer();
        server.getPluginManager().registerEvents(this, this);

        ScheduleCheck("Server started, waiting for Players to join." +
                " Otherwise shutdown in 120s", startDelayCheck);
    }

    @Override
    public void onDisable() {
        // Plugin shutdown logic
    }

    private void addLogEntry(String message)
    {
        server.getLogger().info(message);
    }

    @EventHandler
    public void onPlayerJoin(PlayerJoinEvent playerEvent)
    {
        if(checkPlayerTask!= null)
        {
            checkPlayerTask.cancel();
            addLogEntry("Checking has been aborted");
            checkPlayerTask = null;
        }
        if(shutdownTask != null)
        {
            shutdownTask.cancel();
            addLogEntry("Shutdown aborted");
            shutdownTask = null;
        }
    }

    @EventHandler
    public void onPlayerQuit(PlayerQuitEvent playerEvent)
    {
        ScheduleCheck("Player left, checking if last player", delayCheck);
    }

    private void ScheduleCheck(String message, long delay) {
        addLogEntry(message);
        checkPlayerTask = createTask(createCheckingRunnable(), delay);
    }

    private Runnable createCheckingRunnable() {
       return new Runnable() {
           @Override
           public void run() {
               CheckServer();
           }
       };
    }

    private Runnable createShutdownRunnable()
    {
        return new Runnable() {
            @Override
            public void run() {
                addLogEntry("Shutting down");
                server.shutdown();
            }
        };
    }

    private BukkitTask createTask(Runnable runnable, long delay)
    {
        return server.getScheduler().runTaskLater(this, runnable, delay * 20);
    }

    private void CheckServer()
    {
        if(isServerEmpty())
        {
            addLogEntry("Server shuts down in "+ delayShutdown + " seconds!");

            shutdownTask = createTask(createShutdownRunnable(), delayShutdown);
        }
    }

    private boolean isServerEmpty()
    {
        return getServer().getOnlinePlayers().size() == 0;
    }

}
