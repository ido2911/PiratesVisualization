using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;
using System.Text;

namespace PiratesVisualization
{
    public partial class Form1 : Form
    {
        private ACO_Solver solver;

        private Bitmap graphBitmap;
        private Graphics graphics;

        // Location veriable
        private Point[] islandsPoints;

        // Simulation state variables
        private bool simulationRunning = false;
        private bool visualizeIterations;
        private int currentIteration = 0;
        private Stopwatch stopwatch = new Stopwatch();


        Random random = new Random();

        // List of colors to match each pirate
        private List<Color> piratePathColors = new List<Color>()
        {
            Color.Blue,
            Color.Green,
            Color.Purple,
            Color.Orange,
            Color.YellowGreen,
            Color.Cyan,
            Color.DarkSalmon,
            Color.Olive,
            Color.Teal,
            Color.Navy
        };

        public Form1()
        {
            InitializeComponent();
            this.Resize += Form1_Resize;

            // Hook up the Paint event handler for the PictureBox
            pictureBoxGraph.Paint += PictureBoxGraph_Paint;

            // Hook up the Timer Tick event handler
            simulationTimer.Tick += SimulationTimer_Tick;

            // Init buttons to initial state
            buttonEnd.Enabled = false;
            buttonReset.Enabled = false;
        }

        // Initialize the Bitmap and Graphics object for drawing
        private void InitializeGraphics()
        {
            // Dispose of previous graphics objects if they exist
            graphics?.Dispose();
            graphBitmap?.Dispose();

            if (pictureBoxGraph.ClientSize.Width > 0 && pictureBoxGraph.ClientSize.Height > 0)
            {
                graphBitmap = new Bitmap(pictureBoxGraph.ClientSize.Width, pictureBoxGraph.ClientSize.Height);
                graphics = Graphics.FromImage(graphBitmap);
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            }
        }

        // Handle the Paint event of the PictureBox
        private void PictureBoxGraph_Paint(object sender, PaintEventArgs e)
        {
            // Draw the content from the pre-rendered bitmap
            if (graphBitmap != null)
            {
                e.Graphics.DrawImage(graphBitmap, 0, 0);
            }
        }

        // Handle the Timer Tick event
        private void SimulationTimer_Tick(object sender, EventArgs e)
        {
            Debug.WriteLine("Ticked");
            if (simulationRunning && solver != null)
            {
                // Run one iteration of the ACO algorithm
                solver.RunIteration();

                // Increment iteration count
                currentIteration++;
                labelIterationNum.Text = $"Iteration: {currentIteration}";
                // Update display of best grade
                labelBestGrade.Text = $"best grade: {solver.maxNetValue:F2}";

                // Redraw the visualization
                DrawGraph(); // Call your drawing method
                pictureBoxGraph.Invalidate(); // Trigger the Paint event

                // --- Check if simulation should stop ---
                if (currentIteration >= solver.iterations)
                {
                    FinishSimulation();
                }
            }
        }

        // Handle the form resizing event to re-initialize graphics
        private void Form1_Resize(object sender, EventArgs e)
        {

            // Get the new client size of the form (the area inside the borders and title bar)
            Size newFormClientSize = this.ClientSize;


            int panelRightEdgeX = panel1.Right;

            // The PictureBox should start at the right edge of the panel
            int pictureBoxLocationX = panelRightEdgeX;
            int pictureBoxLocationY = 0; // Assuming the PictureBox starts at the top of the form client area

            // The width of the PictureBox is the total form width minus the space taken by the panel
            int pictureBoxWidth = newFormClientSize.Width - panelRightEdgeX;
            // The height of the PictureBox is the full height of the form client area
            int pictureBoxHeight = newFormClientSize.Height;


            // Ensure the dimensions are not negative (can happen when minimizing)
            if (pictureBoxWidth > 0 && pictureBoxHeight > 0)
            {
                // Set the new size and location of the PictureBox
                pictureBoxGraph.Size = new Size(pictureBoxWidth, pictureBoxHeight);
                pictureBoxGraph.Location = new Point(pictureBoxLocationX, pictureBoxLocationY);

                // Re-initialize graphics with the new PictureBox size
                InitializeGraphics();

                // Only draw if islandsPoints has been initialized (i.e., simulation started at least once)
                if (islandsPoints != null)
                {
                    // Redraw the graph on resize
                    DrawGraph();
                    // Trigger the Paint event to display the updated bitmap
                    pictureBoxGraph.Invalidate();
                }
                else
                {
                    // If islandsPoints is null, just clear the graphics to the background color
                    if (graphics != null)
                    {
                        graphics.Clear(Color.White);
                        pictureBoxGraph.Invalidate();
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Initialize graphics after the form has loaded and PictureBox size is determined
            InitializeGraphics();

            // Initial drawing - clear the background when the form loads
            // The actual graph will be drawn when the simulation starts
            if (graphics != null)
            {
                graphics.Clear(Color.White); // Clear with background color
                pictureBoxGraph.Invalidate(); // Show the cleared background
            }
        }

        private Point[] GenerateRandomNodeLocations(int numberOfIslands, Size containerSize, int minDistance = 20, int maxAttemptsPerNode = 100)
        {
            // Initialize a list to hold the node locations
            Point[] islandLocations = new Point[numberOfIslands];

            // Generate a location for each node
            for (int i = 0; i < numberOfIslands; i++)
            {
                bool locationFound = false;
                int attempts = 0;

                while (!locationFound && attempts < maxAttemptsPerNode)
                {
                    // Generate a random X coordinate within the container width
                    // Subtract minDistance to ensure there's space around the node near edges
                    int x = random.Next(minDistance, containerSize.Width - minDistance);

                    // Generate a random Y coordinate within the container height
                    // Subtract minDistance to ensure there's space around the node near edges
                    int y = random.Next(minDistance, containerSize.Height - minDistance);

                    Point potentialLocation = new Point(x, y);

                    // Check if this potential location is too close to any existing node locations
                    bool tooClose = islandLocations.Any(existingLocation =>
                    {
                        // Calculate the distance between the potential location and an existing location
                        double distance = Math.Sqrt(Math.Pow(potentialLocation.X - existingLocation.X, 2) + Math.Pow(potentialLocation.Y - existingLocation.Y, 2));
                        return distance < minDistance;
                    });

                    // If the location is not too close to any existing nodes, add it to the list
                    if (!tooClose)
                    {
                        islandLocations[i] = potentialLocation;
                        locationFound = true;
                    }

                    attempts++;
                }

                // Handle the case where a location couldn't be found after maxAttemptsPerNode
                if (!locationFound)
                {
                    Debug.WriteLine($"Warning: Could not find a suitable location for node {i} after {maxAttemptsPerNode} attempts.");
                }
            }
            // Return the Array of random node locations
            return islandLocations;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Start/Pause/Continue Button clicked");

            #region pause handler
            // --- Toggle Pause/Continue logic ---
            if (simulationRunning)
            {
                if (solver != null && currentIteration < solver.iterations)
                {
                    // Currently running, so pause the simulation
                    simulationRunning = false;
                    simulationTimer.Stop();
                    buttonStart.Text = "resume"; // Change button text
                    Debug.WriteLine("Simulation Paused");
                    return; // Exit the method after pausing
                }
            }
            else
            {
                if (solver != null && currentIteration < solver.iterations)
                {
                    // Currently paused and simulation hasn't finished, so resume
                    simulationRunning = true;
                    simulationTimer.Start();
                    buttonStart.Text = "pause"; // Change button text
                    Debug.WriteLine("Simulation Resumed");
                    return; // Exit the method after resuming
                }
            }
            #endregion

            // if solver is null the user wants to start the simulation

            Debug.WriteLine("Simulation Started");


            // Get parameters from UI controls
            int numPirates        = (int)numericUpDownPirates.Value;
            int numIslands        = (int)numericUpDownIslands.Value;
            int numIterations     = (int)numericUpDownIterations.Value;
            double numEvaporation = (double)numericUpDownEvaporation.Value;

            Size drawingAreaSize = pictureBoxGraph.ClientSize; // Get the size of your drawing surface

            // Generate the random locations for the nodes
            islandsPoints = GenerateRandomNodeLocations(numIslands, drawingAreaSize);

            // Initialize veriables
            Graph graph = new Graph(numIslands, islandsPoints, drawingAreaSize);
            PirateShip[] pirateShips = Generator.GeneratePirates(numPirates, graph.islands);


            // Initialize ACO colony
            solver = new ACO_Solver(graph, pirateShips, numIterations, numEvaporation);

            simulationRunning = true;
            currentIteration = 0;


            // Determine visualization mode 
            if (checkBoxVisualize != null)
            {
               visualizeIterations = checkBoxVisualize.Checked;
            }
            else
            {
               visualizeIterations = true; // Default to visualizing if checkbox not found
            }

            // start stop watch
            stopwatch.Restart();

            if (visualizeIterations)
            {
                // regular run with visualization
                buttonStart.Text = "pause"; // Change button text
                buttonStart.Enabled = true; //changed now
                buttonEnd.Enabled = true;
                buttonReset.Enabled = true;
                simulationTimer.Start();
            }
            else
            {
                // Instant run mode: Disable pause, run all iterations, then finish
                buttonStart.Enabled = false;
                buttonEnd.Enabled = false;
                buttonReset.Enabled = false;

                // Run all iterations immediately
                solver.Solve();

                currentIteration = solver.iterations;
                labelIterationNum.Text = $"Iteration: {currentIteration} ((Finished))";
                labelBestGrade.Text = $"best grade: {solver.maxNetValue:F2}";

                // Simulation is finished after the loop
                FinishSimulation(); // Call the common finish method
            }
        }

        private void buttonEnd_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("End Button clicked");
            bool wasPaused = true;

            // handels situation where the button was pressed while the simulation was paused
            // if the simulation is running pause it.
            if (simulationRunning)
            {
                wasPaused = false;
                simulationRunning = false; // Pause the simulation logic
                simulationTimer.Stop(); // Stop the timer
            }

            // Ask the user if they want to finish or continue
            DialogResult result = MessageBox.Show("Do you want to finish the simulation?", "Simulation End", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // User chose to finish
                FinishSimulation(); // Call the common finish method
            }
            else
            {
                // if simulation wasn't paused when 
                // button was presses, resume the simulation now
                if (!wasPaused)
                {
                    // User chose to continue
                    simulationRunning = true; // Resume simulation logic
                    simulationTimer.Start(); // Restart the timer
                }

                // Update button states
                buttonStart.Enabled = true;
                buttonEnd.Enabled = true;
                buttonReset.Enabled = true;
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Reset Button clicked");


            simulationRunning = false;
            simulationTimer.Stop();
            currentIteration = 0;
            labelIterationNum.Text = "Iteration:";
            labelBestGrade.Text = $"best grade:";


            // Reset pheromone trails on reset 
            if (solver != null && solver.pheromoneTrails != null)
            {
                int numIslands = solver.pheromoneTrails.GetLength(0);
                for (int i = 0; i < numIslands; i++)
                {
                    for (int j = 0; j < numIslands; j++)
                    {
                        solver.pheromoneTrails[i, j] = 0.0; // Or a small initial pheromone value
                    }
                }
            }

            // Reset ACO state
            solver = null;
            buttonStart.Text = "start";

            // Clear the drawing surface
            graphics.Clear(Color.White);
            pictureBoxGraph.Invalidate();

            buttonStart.Enabled = true;
            buttonEnd.Enabled = false;
            buttonReset.Enabled = false;
        }

        // Common method to handle simulation finishing
        private void FinishSimulation()
        {
            Debug.WriteLine("Finished simulation");

            stopwatch.Stop();
            simulationTimer.Stop();
            simulationRunning = false;
            labelIterationNum.Text = $"Iteration: {currentIteration} (Finished)"; // Update status label

            // Re-enable start button
            buttonStart.Enabled = true;
            buttonEnd.Enabled = false;
            buttonReset.Enabled = true;

            // Reset pheromone trails when simulation finishes
            if (solver != null && solver.pheromoneTrails != null)
            {
                int numIslands = solver.pheromoneTrails.GetLength(0);
                for (int i = 0; i < numIslands; i++)
                {
                    for (int j = 0; j < numIslands; j++)
                    {
                        solver.pheromoneTrails[i, j] = 0.0;
                    }
                }
                // Redraw one last time to show cleared pheromones
                DrawGraph();
                pictureBoxGraph.Invalidate();
            }

            // Generate and display finishing message
            string finishingMessage = GenerateFinishingMessage();
            TimeSpan elapsed = stopwatch.Elapsed;
            if (!visualizeIterations)
                finishingMessage += $"\n\nAlgorithm execution time: {elapsed.TotalMilliseconds:F2} ms";
            else
                finishingMessage += $"\n\nAlgorithm execution time: {elapsed.TotalSeconds:F2} sec";

            // because a lot of pirates might be made, a form was made
            // to make sure all data will be visible in the finished message
            using (ResultsForm resultsForm = new ResultsForm())
            {
                resultsForm.ResultsText = finishingMessage;
                resultsForm.ShowDialog(this); // Show the form modally, parented to this form
            }

            // change solver and start so the user can start the simulation again
            solver = null;
            buttonStart.Text = "start";

            //MessageBox.Show(finishingMessage, "Simulation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // --- Drawing Method ---

        // This method will draw the current state of the ACO simulation
        private void DrawGraph()
        {
            // Ensure graphics object is initialized and island locations are available
            if (graphics == null || solver == null || islandsPoints == null || pictureBoxGraph == null) return;

            Size initialPictureBoxSize = pictureBoxGraph.Size;
            // Clear the drawing surface with the background color
            graphics.Clear(Color.White);

            // Get the number of islands from the islandsPoints array length
            int numIslands = islandsPoints.Length;

            // Calculate scaling factors based on the current PictureBox size relative to the initial size
            float scaleX = (float)pictureBoxGraph.ClientSize.Width / initialPictureBoxSize.Width;
            float scaleY = (float)pictureBoxGraph.ClientSize.Height / initialPictureBoxSize.Height;
            // Use the smaller scale factor to maintain aspect ratio and avoid elements going off-screen
            float scale = Math.Min(scaleX, scaleY);

            #region edges_pheremones

            // --- 1. Draw Edges and Pheromones ---
            // Iterate through the upper triangle of your 2D array to draw each edge once
            for (int i = 0; i < numIslands; i++)
            {
                for (int j = i + 1; j < numIslands; j++)
                {

                    double pheromone = solver.pheromoneTrails[i, j];
                    double cost = solver.edges[i, j];

                    const double PheromoneThreshold = 500; // Use a small threshold for floating point comparison
                    if (pheromone > PheromoneThreshold)
                    {


                        // Calculate line thickness based on pheromone
                        float maxPheromone = 2000; // Adjusted based on expected max pheromone values
                        if (pheromone > maxPheromone)
                            pheromone = maxPheromone; // Clamp pheromone value for scaling

                        // Scale the base and max thickness values
                        float minThickness = 1.0f * scale;
                        float maxThickness = 5.0f * scale;
                        // Scale thickness linearly based on pheromone relative to maxPheromone
                        float thickness = minThickness + (float)(pheromone / maxPheromone) * (maxThickness - minThickness);

                        // pheremone color, more red means more pheremone on connection
                        int redIntensity = (int)(pheromone / maxPheromone * 255);
                        Color edgeColor = Color.FromArgb(255, redIntensity, 0, 0);



                        // Use a Pen to draw the line with calculated color and thickness
                        using (Pen edgePen = new Pen(edgeColor, thickness))
                        {
                            graphics.DrawLine(edgePen, islandsPoints[i], islandsPoints[j]);
                        }

                        // draw edge weight/cost near the edge center
                        PointF midPoint = new PointF((islandsPoints[i].X + islandsPoints[j].X) / 2, (islandsPoints[i].Y + islandsPoints[j].Y) / 2);

                        // Scale the font size
                        float fontSize = 8.0f * scale;
                        using (Font font = new Font("Arial", fontSize))
                        using (Brush brush = new SolidBrush(Color.Black))
                        {
                            // Draw the cost, formatted to one decimal place
                            graphics.DrawString(cost.ToString("F1"), font, brush, midPoint);
                        }
                    }
                }
            }
            #endregion

            #region islands

            // --- 2. Draw Islands (Nodes) ---

            Dictionary<int, Color> startingIslandColors = new Dictionary<int, Color>();
            if (solver.pirateShips != null)
            {
                for (int i = 0; i < solver.pirateShips.Length; i++) 
                {

                    PirateShip pirate = solver.pirateShips[i];
                    if (pirate.StartIsland >= 0 && pirate.StartIsland < numIslands)
                    {
                        // get a color from the colors List
                        Color pathColor = piratePathColors[i % piratePathColors.Count]; // to make sure all colors are in the list
                        // add color to dictionary
                        startingIslandColors.Add(pirate.StartIsland, pathColor);
                    }
                    else
                        Debug.WriteLine($"{pirate.StartIsland} is an invalid start island");
                }
            }


            using (Brush islandBrush = new SolidBrush(Color.White)) // Brush for drawing the island circle
            using (Brush textBrush = new SolidBrush(Color.Black)) // Brush for drawing island grade text
            {
                // Scale the island size and font size
                int islandSize = (int)(10 * scale); // Size of the island circle radius
                float gradeFontSize = 10.0f * scale;
                using (Font font = new Font("Arial", gradeFontSize))
                {
                    // Iterate through the island locations array
                    for (int i = 0; i < numIslands; i++)
                    {
                        Point islandLocation = islandsPoints[i];

                        // Choose the brush based on whether it's a starting island and get the corresponding color
                        Brush currentIslandBrush;
                        if (startingIslandColors.TryGetValue(i, out Color startingColor))
                        {
                            // If it's a starting island, use a new brush with the mapped color
                            currentIslandBrush = new SolidBrush(startingColor);
                        }
                        else
                        {
                            // If it's not a starting island, use the default blue brush
                            currentIslandBrush = islandBrush;
                        }

                        // Draw the island circle (scaled)
                        graphics.FillEllipse(currentIslandBrush, islandLocation.X - islandSize / 2, islandLocation.Y - islandSize / 2, islandSize, islandSize);
                        // Draw a black outline around the island circle (scaled)
                        graphics.DrawEllipse(Pens.Black, islandLocation.X - islandSize / 2, islandLocation.Y - islandSize / 2, islandSize, islandSize);

                        // get island grade (assuming solver.islands is an array/list of Island objects with a Reward property)
                        double grade = solver.islands[i].ExpectedVal(); // Access island grade
                        // Draw island grade next to it (scaled font)
                        graphics.DrawString($"Expected: {grade:F1}", font, textBrush, islandLocation.X + islandSize, islandLocation.Y); // Format grade to one decimal
                    }
                }
            }

            #endregion

            #region pirates

            // --- 3. Draw Pirates (Ants) ---
            using (Brush pirateBrush = new SolidBrush(Color.Red)) // Brush for drawing dynamic pirates
            {
                // Scale the pirate size
                int pirateSize = (int)(6 * scale); // Size of the pirate circle radius

                // positoin each pirate in thier current state
                foreach (var pirate in solver.pirateStates)
                {
                    // Ensure the pirate's current island index is within the bounds of the islandsPoints array
                    if (pirate.currentIslandIndex >= 0 && pirate.currentIslandIndex < numIslands)
                    {
                        Point pirateLocation = islandsPoints[pirate.currentIslandIndex];
                        // Draw the pirate circle at the current island's location (scaled)
                        graphics.FillEllipse(pirateBrush, pirateLocation.X - pirateSize / 2, pirateLocation.Y - pirateSize / 2, pirateSize, pirateSize);
                    }
                }
            }

            // Visualize Paths of All Pirates (Ants)
            Path[] piratePaths = solver.bestPaths;

            if (piratePaths != null)
            {

                // Iterate through each pirate's path
                for (int i = 0; i < piratePaths.Length; i++)
                {

                    Path piratePath = piratePaths[i];

                    // Determine the color for this pirate's path based on the index
                    // Use the modulo operator (%) to cycle through the piratePathColors list
                    Color pathColor = piratePathColors[i % piratePathColors.Count];

                    // Scale the pen thickness
                    using (Pen piratePathPen = new Pen(Color.FromArgb(255, pathColor), 4.0f * scale)) // Use the determined color and scaled thickness
                    {
                        // Ensure the path object and its islandsPath list are not null
                        if (piratePath != null && piratePath.islandsPath != null)
                        {
                            // Iterate through each segment (from, to) in the pirate's path
                            foreach (var segment in piratePath.islandsPath)
                            {
                                int fromNodeIndex = segment.from;
                                int toNodeIndex = segment.to;

                                // Ensure the node indices are valid and within the bounds of islandsPoints
                                if (fromNodeIndex >= 0 && fromNodeIndex < numIslands && toNodeIndex >= 0 && toNodeIndex < numIslands)
                                {
                                    // Get the locations of the 'from' and 'to' islands
                                    Point fromLocation = islandsPoints[fromNodeIndex];
                                    Point toLocation = islandsPoints[toNodeIndex];

                                    // Draw a line segment for this part of the pirate's path (scaled pen)
                                    graphics.DrawLine(piratePathPen, fromLocation, toLocation);
                                }
                            }
                        }
                    }

                }
            }
            #endregion
        }

        // Dispose graphics objects when the form is closed
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            graphics?.Dispose();
            graphBitmap?.Dispose();
        }

        private string GenerateFinishingMessage()
        {
            StringBuilder message = new StringBuilder();

            Path[] paths = solver.bestPaths;
            Island[] islands = solver.islands; 

            if (paths != null && islands != null)
            {
                double totalExpected = 0;
                double totalCollectedActual = 0; 
                List<int> collectedIslandIndices = new List<int>();

                message.AppendLine($"Simulation finished after {currentIteration} iterations.");
                message.AppendLine($"Final Best Grade (maxNetValue): {solver.maxNetValue:F2}"); // Display the ACO's best grade

                message.AppendLine("\n--- Pirate Path Summary ---");

                for (int i = 0; i < paths.Length; i++)
                {
                    message.AppendLine($"\nPirate {i}:");

                    if (paths[i] != null && paths[i].islandsPath != null)
                    {
                        message.AppendLine($"  Path Length: {paths[i].count}");
                        message.AppendLine($"  Total Cost: {paths[i].totalCost:F2}");
                        message.AppendLine($"  Total Expected Value (from Path object): {paths[i].totalExcpectedValue:F2}"); // Assuming Path stores this

                        message.Append("  Visited Islands: ");
                        if (paths[i].islandsPath.Count > 0)
                        {
                            // List the islands visited in order (using the 'to' node of each segment)
                            message.Append(paths[i]);
                            message.AppendLine();

                            double pirateCollected = 0;
                            List<int> pirateCollectedIslands = new List<int>();
                            Random collectionRandom = new Random(); // Use a new Random for this display calculation

                            foreach (var step in paths[i].islandsPath)
                            {
                                int visitedIslandIndex = step.to;
                                if (visitedIslandIndex >= 0 && visitedIslandIndex < islands.Length)
                                {

                                    double chance = collectionRandom.NextDouble();
                                    totalExpected += islands[visitedIslandIndex].ExpectedVal();
                                    if (chance < islands[visitedIslandIndex].pTresure)
                                    {
                                        pirateCollected += islands[visitedIslandIndex].Reward;
                                        pirateCollectedIslands.Add(visitedIslandIndex);
                                    }
                                    else
                                    {
                                        pirateCollected -= islands[visitedIslandIndex].Damage;
                                    }
                                }
                            }
                            totalCollectedActual += pirateCollected; // Sum up for the overall total

                            message.AppendLine($"  Collected (simulated for display): {pirateCollected:F2}");
                            message.Append("  Collected from Islands: ");
                            if (pirateCollectedIslands.Count > 0)
                            {
                                message.AppendLine(string.Join(", ", pirateCollectedIslands));
                            }
                            else
                            {
                                message.AppendLine("None");
                            }
                        }
                        else
                        {
                            message.AppendLine("  Path is empty.");
                        }
                    }
                    else
                    {
                        message.AppendLine("  Path data is not available.");
                    }
                }

                message.AppendLine("\n--- Overall Summary ---");
                message.AppendLine($"Total Expected Value (summed): {totalExpected:F2}"); // If you summed expected values
                message.AppendLine($"Total Collected (simulated for display): {totalCollectedActual:F2}");

                if (totalCollectedActual < 0)
                    message.AppendLine("Unlucky day :(");
            }
            else
            {
                message.AppendLine("Simulation results not available.");
            }

            return message.ToString();
        }

        private void radioButtonACO_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonBT_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
