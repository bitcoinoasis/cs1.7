# Bot Spawn Patterns Visual Guide

## Pattern Examples

### Circle Pattern
```
            B
        B       B
      B           B
    B      S        B
      B           B
        B       B
            B
```
- S = Spawner position
- B = Bot position
- Bots arranged in a circle
- Radius configurable
- All bots face center

**Settings:**
- Spawn Pattern: Circle
- Spawn Radius: 10
- Number Of Bots: 10

**Use Case:** 360° aim practice, testing range abilities

---

### Line Pattern
```
S → B B B B B B B B B B
```
- Bots in a straight line
- All facing same direction
- Spacing configurable

**Settings:**
- Spawn Pattern: Line
- Bot Spacing: 2
- Number Of Bots: 10

**Use Case:** Target practice, spray control, weapon testing

---

### Grid Pattern
```
    B B B B
    B B B B
S   B B B B
    B B B B
```
- Bots arranged in rows and columns
- Automatically calculates best grid
- Even spacing

**Settings:**
- Spawn Pattern: Grid
- Bot Spacing: 2
- Number Of Bots: 16

**Use Case:** Multiple targets, splash damage testing, crowd control abilities

---

### Random Pattern
```
        B
    B       B
  B   S   B
      B       B
    B     B
```
- Random positions within radius
- Each spawn is unique
- Unpredictable layout

**Settings:**
- Spawn Pattern: Random
- Spawn Radius: 10
- Number Of Bots: 10

**Use Case:** Realistic scenarios, movement practice, finding targets

---

### Custom Points Pattern
```
You define the positions:

Transform1 → B
Transform2 → B
Transform3 → B
Transform4 → B
```
- Use any GameObject positions
- Precise placement
- Flexible layouts

**Settings:**
- Spawn Pattern: CustomPoints
- Custom Spawn Points: [Array of Transforms]

**Use Case:** Specific scenarios, map-based spawns, complex layouts

---

## Example Setups

### Shooting Range Setup
```
Player Position: (0, 1, 0)
BotSpawner Position: (0, 0, 20)
Pattern: Line
Number: 5
Spacing: 3

Result: 5 bots in a line 20 units ahead
```

### Arena Setup
```
Player Position: (0, 1, 0)
BotSpawner Position: (0, 0, 0)
Pattern: Circle
Number: 12
Radius: 15

Result: Player surrounded by 12 bots
```

### Practice Course
```
Use 3 different spawners:

Spawner1: Circle pattern (close range)
Spawner2: Line pattern (mid range)
Spawner3: Grid pattern (far range)
```

---

## Gizmo Visualization

In the Unity Editor, BotSpawner shows:
- **Circle Pattern**: Cyan wire circle showing spawn area
- **Random Pattern**: Cyan wire cube showing spawn bounds
- Visual preview before playing

Enable Gizmos in Scene view to see spawn areas!

---

## Hot Keys

- **F5**: Spawn bots (runtime)
- **F6**: Clear all bots (runtime)

Can spawn/clear repeatedly during play mode!

---

## Tips

1. **Start Simple**: Use Circle pattern with 5-10 bots
2. **Test Range**: Adjust spawn radius to test weapon range
3. **Multiple Spawners**: Use several spawners for varied targets
4. **Bot Health**: Lower health (50) for easier kills, higher (200) for challenge
5. **Respawn**: Enable respawn for continuous practice
6. **Colors**: Use different colors per spawner to identify targets

---

## Advanced Usage

### Progressive Difficulty
```
Spawner1: Close (Radius: 5), Low HP (50)
Spawner2: Medium (Radius: 10), Normal HP (100)
Spawner3: Far (Radius: 20), High HP (150)
```

### Movement Practice
```
Pattern: Random
Respawn: Disabled
Hunt down all targets
```

### Ability Testing
```
Pattern: Grid
Number: 16
Close together
Test AOE abilities
```

### Headshot Practice
```
Pattern: Line
Number: 5
Spacing: 5
Practice precision
```

---

## Script Reference

```csharp
// Spawn bots from code
BotSpawner spawner = FindObjectOfType<BotSpawner>();
spawner.SpawnBots();

// Clear all bots
spawner.ClearBots();

// Change pattern at runtime
spawner.spawnPattern = BotSpawner.SpawnPattern.Circle;
spawner.numberOfBots = 20;
spawner.SpawnBots();
```

---

**Experiment with different patterns to create the perfect practice environment!** 🎯
